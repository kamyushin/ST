using app;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app {
    public class FlowListBase
    {
        protected List<FlowDefine.FlowType> FlowList = new List<FlowDefine.FlowType>();

        protected virtual void SetFlowList(){}

        public void StartFlowList()
        {
        }

        public void OnFlowStart(FlowDefine.FlowType flowType)
        {
            switch (flowType)
            {
                case FlowDefine.FlowType.Title:
                    break;
                case FlowDefine.FlowType.Game:
                    break;
            }
        }

        private void OnTitleStart()
        {

        }

        private void OnGameStart()
        {

        }
    }
}
