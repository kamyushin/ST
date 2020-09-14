using UnityEngine;

namespace app.GameCamera
{
    public class CameraController : MonoBehaviour
    {

        public enum Mode {
            Enemy
        }

        [SerializeField]
        private new Camera camera;

        [System.NonSerialized]
        public Mode mode = Mode.Enemy;
        
        [SerializeField]
        public CameraModeParameter enemyModeParameter;

        Transform CachedTransform
        {
            get
            {
                if (cachedTransform == null)
                {
                    cachedTransform = GetComponent<Transform>();
                }
                return cachedTransform;
            }
        }

        Transform cachedTransform;

        private float cachedFieldOfView = 0.0f;
        private Vector3 cachedPosition = Vector3.zero;
        private Quaternion cachedRotation = Quaternion.identity;

        void Awake() {
            if (camera == null) { camera = GetComponent<Camera>(); }
            cachedFieldOfView = enemyModeParameter.fieldOfView;
            cachedPosition = CachedTransform.localPosition;
            cachedRotation = CachedTransform.localRotation;
        }

        void Update()
        {
            switch (mode)
            {
                case Mode.Enemy:
                {
                    if (CachedTransform == null) { return; }
                    if (enemyModeParameter.targetTransform == null) { return; }
                    if (enemyModeParameter.switcherTransform == null) { return; }

                    //プレイヤーから敵への向きを算出
                    var enemyTransform = enemyModeParameter.targetTransform;
                    var playerTransform = enemyModeParameter.switcherTransform;
                    var direction = enemyTransform.localPosition - playerTransform.localPosition;
                    direction.y = 0.0f;
                    var directionRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
                    
                    //キャラクターの現在地に、敵への向きにオフセット座標を加算したものを座標に適用
                    var position = playerTransform.localPosition + directionRotation * enemyModeParameter.offsetPosition;
                    
				    // キャラクターから敵への向きに、オフセット回転を加算したものを回転に適応
				    var rotation = directionRotation * Quaternion.Euler(enemyModeParameter.offsetRotation);

				    var fieldOfView = enemyModeParameter.fieldOfView;
				    var ratio = enemyModeParameter.lerpRatio * Time.deltaTime;

				    // 線形補間
				    cachedPosition = Vector3.Lerp(cachedPosition, position, ratio);
				    cachedRotation = Quaternion.Slerp(cachedRotation, rotation, ratio);
				    cachedFieldOfView = Mathf.Lerp(cachedFieldOfView, fieldOfView, ratio);

				    // 本体に入力
				    CachedTransform.position = cachedPosition;
				    CachedTransform.rotation = cachedRotation;
				    camera.fieldOfView = cachedFieldOfView;
                }
                    break;
                default:
                    break;
            }
        }
    }
}
