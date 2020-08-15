using UnityEngine;

namespace Player 
{
    public class PlayerController : MonoBehaviour
    {
        //移動距離
        [SerializeField]float moveDistance = 0.1f;

        //リジットボディ
        [SerializeField]Rigidbody rb;
        //接地判定
        [SerializeField]bool isGrounded;
        //飛ぶモード
        [SerializeField]bool isFlying;
        //ジャンプ力
        public float jumpPower = 400f;

        void Start()
        {
            rb = GetComponent<Rigidbody>();

        }

        void Update()
        {
            //地面に触れている場合
            if (isGrounded)
            {
                if (!isFlying && Input.GetKeyDown(KeyCode.Space))
                {
                    isGrounded = false;
                    //上にジャンプする
                    rb.AddForce(Vector3.up * jumpPower);
                }
            }

            //左移動
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-moveDistance, 0, 0);
            }

            //右移動
           if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(moveDistance, 0, 0);
            }

            //前(上)移動
            if (Input.GetKey(KeyCode.W))
            {
                if(!isFlying)
                {
                    transform.Translate(0, 0, moveDistance);
                }
                else
                {
                    transform.Translate(0, moveDistance, 0);
                }
            }

            //後ろ(下)移動
            if (Input.GetKey(KeyCode.S))
            {
                if(!isFlying)
                {
                    transform.Translate(0, 0, -moveDistance);
                }
                else if(!isGrounded)
                {
                    transform.Translate(0, -moveDistance, 0);
                }
            }

            //飛ぶモード変更
            if(Input.GetKeyDown(KeyCode.F))
            {
                if(!isFlying)
                {
                    isFlying = true;
                    rb.useGravity = false;
                }
                else
                {
                    isFlying = false;
                    rb.useGravity = true;
                }
            }
        }

        //地面に触れた時
        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Ground")
            {
                isGrounded = true;
            }
        }

        void OnCollisionExit(Collision other) {
            if (other.gameObject.tag == "Ground")
            {
                isGrounded = false;
            }
        }
    }
}
