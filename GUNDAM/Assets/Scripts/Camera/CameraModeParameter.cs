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
        //注視点からのオフセット座標
        public Vector3 offsetPosition;
        //注視点からのオフセット回転
        public Vector3 offsetRotation;
        //カメラの見通し
        public float fieldOfView = 60.0f;
        //座標や回転の補間率
        public float lerpRatio = 55.0f;
    }
}


