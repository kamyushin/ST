using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app {
    public class FlowMap
    {
        protected List<FlowDefine.FlowType> ManageFlowList = new List<FlowDefine.FlowType>();

        private int CurrentIndex = 0;

        public void StartFlowMap()
        {
            if ( ManageFlowList.Count > 0)
            {
                FlowManager.Instance.RequestLoad(ManageFlowList[0]);
            }
        }

        public void Next()
        {

        }

    }
}
