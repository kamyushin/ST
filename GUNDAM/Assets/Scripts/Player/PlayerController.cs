using UnityEngine;

namespace app.Player 
{
    public class PlayerController : MonoBehaviour
    {
        //移動距離
        [SerializeField]float moveDistance = 0.1f;
        //飛ぶ距離
        [SerializeField]float flyDistance = 0.15f;

        //CameraController
        [SerializeField]
        app.GameCamera.CameraController cameraController = null;

        //CharacterController
        [SerializeField]
        CharacterController characterController = null;

        //移動速度(unit/sec)
        [SerializeField]
        float moveUnitPerSec = 1.0f;
        //移動目的地
        Vector3 moveDestination;
        //移動方向
        Quaternion moveDirection;
        //重力加速度(unit/sec^2)
        [SerializeField]
        float gravityUnitPerSecSquare = -0.098f;
        //重力
        [SerializeField]Vector3 gravity;

        // //地面から離れた時間
        // [SerializeField]float isNotGroundedTime = 0f;
        //飛行フラグ
        [SerializeField]bool flyFlag = false;
        //飛行必要フレーム
        const int flyNeedFrame = 10;
        //飛行猶予フレーム
        [SerializeField]int flyGraceFrame = 0;

        //移動ボタン2連続押し猶予フレーム
        [SerializeField]int doubleTapGraceFrame = 5;
        //ブーストダッシュ判定するためのフラグ
        [SerializeField]bool dashJudgeFlag = false;
        //ブーストダッシュ必要フレーム
        const int dashNeedFrame = 5;
        //ブーストダッシュ猶予フレーム
        [SerializeField]int dashGraceFrame = 0;

        //ステップ必要フレーム
        const int stepNeedFrame = 5;
        //ステップ猶予フレーム
        [SerializeField]int stepGraceFrame = 0;

        //ブーストメーター
        [SerializeField]float boostMeter = 60.0f;

        //左ボタン入力フラグ
        [SerializeField]bool leftButtonFlag = false;
        //右ボタン入力フラグ
        [SerializeField]bool rightButtonFlag = false;
        //上ボタン入力フラグ
        [SerializeField]bool upButtonFlag = false;
        //下ボタン入力フラグ
        [SerializeField]bool downButtonFlag = false;
        //飛んでるフラグ
        [SerializeField]bool isFlying = false;

        void FixedUpdate()
        {

            //真上3秒
            //横移動5秒
            //△攻撃は10分の1くらい減る
            //全部なくなってからも△攻撃のダッシュはできる
            //空中でも地上でも動きは一緒
            //地上かつダッシュしてない時に回復(0.3sくらい?)

            //カメラは自分が常に手前、敵が常に奥にいるような感じで
            //動いてる向きと多少反対になる
            //ダッシュ移動の時は重力消える
            //移動押しっぱで離して直ぐ押してもダッシュする
            //何かブーストを使う系の物は、最初に少しだけ多めに消費する(△攻撃の最初のダッシュだけ例外)

            //ボタンfalseフラグ
            changeMoveButtonFalse();

            //左系ボタン入力
            if(Input.GetAxis("Axis 1") < 0 || Input.GetAxis("Axis 7") < 0 || Input.GetAxis("Horizontal") < 0)
            {
                leftButtonFlag = true;
            }

            //右系ボタン入力
            if(Input.GetAxis("Axis 1") > 0 || Input.GetAxis("Axis 7") > 0 || Input.GetAxis("Horizontal") > 0)
            {
                rightButtonFlag = true;
            }

            //上系ボタン入力
            if(Input.GetAxis("Axis 2") < 0 || Input.GetAxis("Axis 8") < 0 || Input.GetAxis("Vertical") < 0)
            {
                upButtonFlag = true;
            }

            //下系ボタン入力
            if(Input.GetAxis("Axis 2") > 0 || Input.GetAxis("Axis 8") > 0 || Input.GetAxis("Vertical") > 0)
            {
                downButtonFlag = true;
            }

            //左ボタン
            if(leftButtonFlag)
            {
                playerMove(new Vector3(-moveDistance, 0, 0));
            }

            //右ボタン
            if(rightButtonFlag)
            {
                playerMove(new Vector3(moveDistance, 0, 0));
            }

            //上ボタン
            if(upButtonFlag)
            {
                playerMove(new Vector3(0, 0, -moveDistance));
            }

            //下ボタン
            if(downButtonFlag)
            {
                playerMove(new Vector3(0, 0, moveDistance));
            }

            //目的地との距離を計算
            var position = transform.localPosition;
            //目的地までのベクトル
            var difference = moveDestination - position;
            //XZ軸上の平面上の移動の場合Y成分を消す

            //FIXME ここがこれじゃダメな気がする
            //移動距離が小さすぎなければ移動
            var speed = 0.0f;
            //目的地までの距離
            var distance = difference.magnitude;

            //ボタンを押してる場合
            if(moveButtonFlag())
            {
                //動くスピードを計算
                if(0.0f != Time.deltaTime) { speed = moveUnitPerSec * Time.deltaTime; }
                speed = speed < distance ? speed : distance;

                //計算した後、向きのためにNormalize
                difference.Normalize();
                //向きの決定
                moveDirection = Quaternion.LookRotation(difference);
            }

            //キャラクターの回転
            transform.localRotation = Quaternion.Lerp(transform.localRotation, moveDirection, 5.0f * Time.deltaTime);

            //キャラクターの移動(目的地までのベクトル * スピード + 重力ベクトル)
            var moveDifference = difference * speed + gravity;
            characterController.Move(moveDifference);

            //Rayを飛ばしての接地判定
            const float rayDistance = 1.2f;
            var rayPosition = transform.localPosition + new Vector3(0.0f, 0.1f, 0.0f);
            var rayLayerMask = LayerMask.GetMask("Ground");
            var ray = new Ray(rayPosition, Vector3.down);
            var isGrounded = Physics.Raycast(ray, rayDistance, rayLayerMask);

            //接地してる or 飛んでる
            if(isGrounded || isFlying)
            {
                //重力を0に
                gravity.y = 0.0f;
            }
            else
            {
                //重力を足していく
                gravity.y += gravityUnitPerSecSquare * Time.deltaTime;
            }

            //○ボタン
            if(Input.GetButtonDown("○"))
            {
                Debug.Log("ChangeTarget or Submit");
            }


            //×ボタン
            if(Input.GetButton("×"))
            {
                //飛ぶ
                isFlying = true;
            } else {
                isFlying = false;
            }

            //△ボタン
            if(Input.GetButtonDown("△"))
            {
                Debug.Log("Attack");
            }

            //□ボタン
            if(Input.GetButtonDown("□"))
            {
                Debug.Log("Shoot");
            }

            //R1ボタン
            if(Input.GetButtonDown("R1"))
            {
                Debug.Log("SubShoot");
            }

            //R2ボタン
            if(Input.GetButtonDown("R2"))
            {
                Debug.Log("SpecialAttack");
            }

            //L1ボタン
            if(Input.GetButtonDown("L1"))
            {
                Debug.Log("Communication");
            }

            //L2ボタン
            if(Input.GetButtonDown("L2"))
            {
                Debug.Log("SpecialShoot");
            }

            //R3ボタン
            if(Input.GetButtonDown("R3"))
            {
                Debug.Log("EXBurst");
            }
        }

        bool moveButtonFlag()
        {
            if(leftButtonFlag || rightButtonFlag || upButtonFlag || downButtonFlag)
            {
                return true;
            }
            return false;
        }

        //移動ボタンfalse
        void changeMoveButtonFalse()
        {
            leftButtonFlag = false;
            rightButtonFlag = false;
            upButtonFlag = false;
            downButtonFlag = false;
        }

        //移動
        void playerMove(Vector3 direction)
        {
            if(this == null) { return; }

            //移動速度
            var length = moveUnitPerSec * Time.deltaTime;

            //カメラの向く方向の指定
            var cameraForward = cameraController.transform.forward;

            //カメラを指定した方向に向ける
            var cameraRotation = Quaternion.LookRotation(cameraForward);
            var directionXZ = direction;
            var directionFromCamera = cameraRotation * directionXZ;
            directionFromCamera.Normalize();

            //目的地の決定
            moveDestination = directionFromCamera * length;
            moveDestination += transform.localPosition;
        }
    }
}
