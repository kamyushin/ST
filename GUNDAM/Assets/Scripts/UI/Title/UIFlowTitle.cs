using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app.UI
{
    public class UIFlowTitle : UIFlowBase
    {
        private float DecideTimer = 1.0f;
        private bool IsDecide = false;

        public static  string UIName = "Title";

        public static UIHandle StartFlow()
        {
            if (UIFlowManager.IsInstanceEnable)
            {
                var handle = UIFlowManager.Instance.StartFlow(UIName);
                return handle;
            }

            return null;
        }

        // Update is called once per frame
        void Update()
        {
            if (Handle == null || Handle.End) return;

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
                    Handle.End = true;
                }
            }

            if (Handle.End) Destroy(this.gameObject);

        }
    }
}
