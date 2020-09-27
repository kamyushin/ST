using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
    [DefaultExecutionOrder((int)ExecutionOrder.INPUT)]
    public class InputController : SingletonBehaviour<InputController>
    {
        public bool left = false;
        public bool right = false;
        public bool up = false;
        public bool down = false;
        public bool circle = false;
        public bool cross = false;
        public bool triangle = false;
        public bool square = false;
        public bool l1 = false;
        public bool l2 = false;
        public bool l3 = false;
        public bool r1 = false;
        public bool r2 = false;
        public bool r3 = false;
        public bool option = false;

        protected override void doUpdate() {
            inputReset();

            //左系ボタン入力
            if(Input.GetAxis("Axis 1") < 0 || Input.GetAxis("Axis 7") < 0 || Input.GetAxis("Horizontal") < 0)
            {
                left = true;
            }

            //右系ボタン入力
            if(Input.GetAxis("Axis 1") > 0 || Input.GetAxis("Axis 7") > 0 || Input.GetAxis("Horizontal") > 0)
            {
                right = true;
            }

            //上系ボタン入力
            if(Input.GetAxis("Axis 2") < 0 || Input.GetAxis("Axis 8") < 0 || Input.GetAxis("Vertical") < 0)
            {
                up = true;
            }

            //下系ボタン入力
            if(Input.GetAxis("Axis 2") > 0 || Input.GetAxis("Axis 8") > 0 || Input.GetAxis("Vertical") > 0)
            {
                down = true;
            }

            //○ボタン
            if(Input.GetButtonDown("○"))
            {
                circle = true;
            }

            //×ボタン
            if(Input.GetButton("×"))
            {
                cross = true;
            }

            //△ボタン
            if(Input.GetButtonDown("△"))
            {
                triangle = true;
            }

            //□ボタン
            if(Input.GetButtonDown("□"))
            {
                square = true;
            }

            //L1ボタン
            if(Input.GetButtonDown("L1"))
            {
                l1 = true;
            }

            //L2ボタン
            if(Input.GetButtonDown("L2"))
            {
                l2 = true;
            }

            //L2ボタン
            if(Input.GetButtonDown("L2"))
            {
                l3 = true;
            }

            //R1ボタン
            if(Input.GetButtonDown("R1"))
            {
                r1 = true;
            }

            //R2ボタン
            if(Input.GetButtonDown("R2"))
            {
                r2 = true;
            }

            //R3ボタン
            if(Input.GetButtonDown("R3"))
            {
                r3 = true;
            }
        }

        void inputReset()
        {
            left = false;
            right = false;
            up = false;
            down = false;
            circle = false;
            cross = false;
            triangle = false;
            square = false;
            l1 = false;
            l2 = false;
            l3 = false;
            r1 = false;
            r2 = false;
            r3 = false;
        }
    }
}
