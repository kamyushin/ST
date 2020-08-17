using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace app
{
    public class FlowBoot : MonoBehaviour
    {
        public FlowDefine.GameFlowType BootFlowType = FlowDefine.GameFlowType.Title;

        // Start is called before the first frame update
        void Start()
        {
            if (FlowManager.IsInstanceEnable)
            {
                FlowManager.Instance.RequestLoad(BootFlowType);
            }

            Destroy(this.gameObject);
        }
    }
}
