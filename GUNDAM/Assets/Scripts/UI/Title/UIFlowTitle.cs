using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app.UI
{
    public class UIFlowTitle : UIFlowBase
    {
        public static  string UIName = "Title";

        public static bool IsDecide = false;

        public static UIHandle StartFlow()
        {
            if (UIFlowManager.IsInstanceEnable)
            {
                var handle = UIFlowManager.Instance.StartFlow(UIName);
                return handle;
            }

            return null;
        }

        public static void DestroyFlow()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Handle == null) return;

            
            if (Input.anyKeyDown)
            {
                Controller.SetState("Decide");
                IsDecide = true;
            }

            if (Handle.End)
            {
                Destroy(gameObject);
            }
        }
    }
}
