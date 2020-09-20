using UnityEngine;

namespace app.GameCamera
{
    [System.Serializable]
    public class CameraModeParameter
    {
        //モード
        public CameraController.Mode mode;
        //注視点
        public Transform targetTransform;
        //モード切替を行ったGameObjectのTransform
        public Transform switcherTransform;
        //プレイヤーのオフセット位置
        public Vector3 offsetPosition;
        //プレイヤーのオフセット回転
        public Vector3 offsetRotation;
        //ターゲットのオフセット位置
        public Vector3 targetOffsetPosition;
        //カメラの見通し
        public float fieldOfView = 60.0f;
        //座標や回転の補間率
        public float lerpRatio = 55.0f;
    }
}


