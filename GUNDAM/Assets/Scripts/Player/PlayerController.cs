using UnityEngine;

namespace Player 
{
    public class PlayerController : MonoBehaviour
    {
        //移動距離
        [SerializeField]float moveDistance = 0.1f;
        //飛ぶ距離
        [SerializeField]float flyDistance = 0.15f;

        //リジットボディ
        [SerializeField]Rigidbody rb;
        //重力
        [SerializeField]Vector3 localGravity = new Vector3(0, -9.8f, 0);
        //接地判定
        [SerializeField]bool isGrounded;

        //地面から離れた時間
        [SerializeField]float isNotGroundedTime = 0f;
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

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
        }

        void FixedUpdate()
        {

            //重力後回し
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

            //左
            if(leftButtonFlag)
            {
                transform.Translate(-moveDistance, 0, 0);
            }

            //右
            if(rightButtonFlag)
            {
                transform.Translate(moveDistance, 0, 0);
            }

            //上
            if(upButtonFlag)
            {
                transform.Translate(0, 0, moveDistance);
            }

            //下
            if(downButtonFlag)
            {
                transform.Translate(0, 0, -moveDistance);
            }

            //○ボタン
            if(Input.GetButtonDown("○"))
            {
                Debug.Log("ChangeTarget or Submit");
            }


            //×ボタン
            if(Input.GetButton("×"))
            {
                if(flyGraceFrame < flyNeedFrame)
                {
                    flyGraceFrame++;
                }
                else
                {
                    flyFlag = true;
                }
            }
            else
            {
                flyGraceFrame = 0;
                flyFlag = false;
            }

            //地面にいない && 飛んでない
            if(!isGrounded && !flyFlag)
            {
                transform.Translate(0, -flyDistance, 0);
            }

            //飛んでる
            if(flyFlag)
            {
                transform.Translate(0, flyDistance, 0);
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

        //移動ボタンfalse
        void changeMoveButtonFalse()
        {
            leftButtonFlag = false;
            rightButtonFlag = false;
            upButtonFlag = false;
            downButtonFlag = false;
        }

        //地面に触れた時
        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Ground")
            {
                isGrounded = true;
                isNotGroundedTime = 0f;
            }
        }

        void OnCollisionExit(Collision other)
        {
            if (other.gameObject.tag == "Ground")
            {
                isGrounded = false;
                isNotGroundedTime = 0f;
            }
        }
    }
}
