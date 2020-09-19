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
        //カメラモード
        [System.NonSerialized]
        public Mode mode = Mode.Enemy;
        //各カメラモードに対するパラメータの設定
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

        void Awake() {
            if (camera == null) { camera = GetComponent<Camera>(); }
        }

        void FixedUpdate()
        {
            switch (mode)
            {
                case Mode.Enemy:
                {
                    if (CachedTransform == null) { return; }
                    if (enemyModeParameter.targetTransform == null) { return; }
                    if (enemyModeParameter.switcherTransform == null) { return; }

                    //敵とプレイヤーの位置
                    var enemyTransform = enemyModeParameter.targetTransform;
                    var playerTransform = enemyModeParameter.switcherTransform;
                    //敵のプレイヤーとの距離を計算
                    var direction = enemyTransform.localPosition - playerTransform.localPosition + enemyModeParameter.targetOffsetPosition;
                    //距離を元に回転を計算
                    var directionRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);

                    //キャラクターの現在地に、敵への向きにオフセット座標を加算したものを座標に適用
                    var position = playerTransform.localPosition + directionRotation * enemyModeParameter.offsetPosition;

				    //キャラクターから敵への向きに、オフセット回転を加算したものを回転に適応
				    var rotation = directionRotation * Quaternion.Euler(enemyModeParameter.offsetRotation);

                    //見た目と割合
				    var fieldOfView = enemyModeParameter.fieldOfView;
				    var ratio = enemyModeParameter.lerpRatio * Time.deltaTime;

				    //線形補間
				    CachedTransform.position = Vector3.Lerp(CachedTransform.position, position, ratio);
				    CachedTransform.rotation = Quaternion.Slerp(CachedTransform.rotation, rotation, ratio);
				    camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, fieldOfView, ratio);
                }
                    break;
                default:
                    break;
            }
        }
    }
}
