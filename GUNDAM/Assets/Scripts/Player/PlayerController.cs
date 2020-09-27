using UnityEngine;

namespace app.Player 
{
    [DefaultExecutionOrder((int)ExecutionOrder.PLAYER)]
    public class PlayerController : MonoBehaviour
    {
        InputController inputController;
        //移動スピード
        [SerializeField]float moveSpeed = 0.3f;
        //飛ぶスピード
        [SerializeField]float flySpeed = 0.1f;

        //CameraController
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
        Vector3 gravity;

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

        //飛んでるフラグ
        [SerializeField]bool isFlying = false;
        
        [SerializeField]Animator animator;

        void Start()
        {
            inputController = GameObject.Find("InputController").GetComponent<InputController>();
            cameraController = GameObject.FindWithTag("MainCamera").GetComponent<GameCamera.CameraController>();
        }

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

            animator.SetBool("is_walking", false);

            //キャラクターの移動
            playerMove();

            //接地してる or 飛んでる
            if(characterController.isGrounded || isFlying)
            {
                //重力を0に
                gravity.y = 0.0f;
            }
            else
            {
                //重力を足していく
                gravity.y += gravityUnitPerSecSquare * Time.deltaTime;
            }

            //×ボタン
            if(inputController.cross)
            {
                //飛ぶ
                isFlying = true;
                transform.localPosition += new Vector3(0, flySpeed, 0);
            } else {
                isFlying = false;
            }
        }

        //キャラクターの移動
        void playerMove()
        {
            //移動距離
            var moveVector = new Vector2(0, 0);

            //左ボタン
            if(inputController.left)
            {
                animator.SetBool("is_walking", true);
                moveVector.x -= moveSpeed;
            }

            //右ボタン
            if(inputController.right)
            {
                animator.SetBool("is_walking", true);
                moveVector.x += moveSpeed;
            }

            //上ボタン
            if(inputController.up)
            {
                animator.SetBool("is_walking", true);
                moveVector.y -= moveSpeed;
            }

            //下ボタン
            if(inputController.down)
            {
                animator.SetBool("is_walking", true);
                moveVector.y += moveSpeed;
            }

            //移動距離
            var length = moveUnitPerSec * Time.deltaTime;

            //カメラの向きを取得
            var cameraForward = cameraController.transform.forward;

            //移動先の向きをカメラの向きに対して、directionの向きにする
            var cameraRotation = Quaternion.LookRotation(cameraForward);
            var directionXZ = new Vector3(moveVector.x, 0.0f, moveVector.y);
            var directionFromCamera = cameraRotation * directionXZ;

            //移動先の向きのy方向を0にしてNormalize
            directionFromCamera.y = 0.0f;
            directionFromCamera.Normalize();

            //移動先ベクトルの決定
            moveDestination = directionFromCamera * length;
            moveDestination += transform.localPosition;

            //目的地との距離を計算
            var position = transform.localPosition;
            //目的地までのベクトル
            var difference = moveDestination - position;

            //移動スピード
            var speed = 0.0f;
            //目的地までの距離
            var distance = difference.magnitude;

            //動くスピードを計算
            if(0.0f != Time.deltaTime) { speed = moveUnitPerSec * Time.deltaTime; }
            speed = speed < distance ? speed : distance;

            //計算した後、向きのためにNormalize
            difference.Normalize();
            if(difference.magnitude != 0)
            {
                //向きの決定
                moveDirection = Quaternion.LookRotation(difference);
            }

            //キャラクターの回転
            transform.localRotation = Quaternion.Lerp(transform.localRotation, moveDirection, 5.0f * Time.deltaTime);

            //キャラクターの移動(目的地までのベクトル * スピード + 重力ベクトル)
            var moveDifference = difference * speed + gravity;
            characterController.Move(moveDifference);
        }
    }
}
