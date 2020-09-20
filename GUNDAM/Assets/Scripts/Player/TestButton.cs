using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app.Player 
{
    public class TestButton : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            //○ボタン or l
            if(Input.GetButtonDown("○")) {
                Debug.Log("ChangeTarget or Submit");
            }

           //×ボタン or k
            if(Input.GetButtonDown("×")) {
                Debug.Log("Jump or Cancel");
            }

            //△ボタンor i
            if(Input.GetButtonDown("△")) {
                Debug.Log("Attack");
            }

            //□ボタン or j
            if(Input.GetButtonDown("□")) {
                Debug.Log("Shoot");
            }

            //R1ボタン or o
            if(Input.GetButtonDown("R1")) {
                Debug.Log("SubShoot");
            }

            //R2ボタン or 0
            if(Input.GetButtonDown("R2")) {
                Debug.Log("SpecialAttack");
            }

            //L1ボタン or q
            if(Input.GetButtonDown("L1")) {
                Debug.Log("Communication");
            }

            //L2ボタン or 1
            if(Input.GetButtonDown("L2")) {
                Debug.Log("SpecialShoot");
            }

            //R3ボタン or m
            if(Input.GetButtonDown("R3")) {
                Debug.Log("EXBurst");
            }

            //OPTIONボタン
            if(Input.GetButtonDown("OPTION")) {
                Debug.Log("Pause");
            }

            //アナログスティック左右 or キーボードa/d
            if(Input.GetAxis("Horizontal") < 0) {
                float h = Input.GetAxis("Horizontal");
		        float v = Input.GetAxis("Vertical");
		        Debug.Log("Horizontal: " + h+","+v);
            }

            //アナログスティック上下 or キーボードw/s
            if(Input.GetAxis("Vertical") != 0) {
                float h = Input.GetAxis("Horizontal");
		        float v = Input.GetAxis("Vertical");
		        Debug.Log("Vertical: " + h+","+v);
            }

            //十字キー左右(positive右)
            if(Input.GetAxis("Axis 1") != 0) {
                float h = Input.GetAxis("Axis 1");
		        float v = Input.GetAxis("Axis 2");
		        Debug.Log("Axis 1: " + h+","+v);
            }

            //十字キー上下(positive下)
            if(Input.GetAxis("Axis 2") != 0) {
                float h = Input.GetAxis("Axis 1");
		        float v = Input.GetAxis("Axis 2");
		        Debug.Log("Axis 2: " + h+","+v);
            }

            //十字キー左右(positive右)
            if(Input.GetAxis("Axis 7") != 0) {
                float h = Input.GetAxis("Axis 7");
		        float v = Input.GetAxis("Axis 8");
		        Debug.Log("Axis 7: " + h+","+v);
            }

            //十字キー上下(positive下)
            if(Input.GetAxis("Axis 8") != 0) {
                float h = Input.GetAxis("Axis 7");
		        float v = Input.GetAxis("Axis 8");
		        Debug.Log("Axis 8: " + h+","+v);
            }
        }
    }
}
