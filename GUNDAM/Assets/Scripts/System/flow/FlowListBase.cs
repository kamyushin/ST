using app;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app {
    public class FlowListBase
    {
        protected List<FlowDefine.FlowType> FlowList = new List<FlowDefine.FlowType>();

        uint CurrentFrowIndex = 0;
        private void Next()
        {
            CurrentFrowIndex++;
            if ( FlowList.Count <= CurrentFrowIndex)
            {
                return;
            }
        }
    }
}
