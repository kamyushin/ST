using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app.UI
{
    public class UIFlowTitle : UIFlowBase
    {
        private float DecideTimer = 3.0f;
        private bool IsDecide = false;

        //TODO Endが強要できない。
        public static bool End { get; protected set; } = false;

        // Update is called once per frame
        void Update()
        {
            if (End) return;

            if (!IsDecide)
            {
                if (Input.anyKeyDown)
                {
                    Controller.SetState("Decide");
                    IsDecide = true;
                }
            }
            else
            {
                DecideTimer -= Time.deltaTime;
                if (DecideTimer <= 0)
                {
                    End = true;
                }
            }

        }
    }
}
