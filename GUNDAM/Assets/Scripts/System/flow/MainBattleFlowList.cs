using app;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
    public class MainBattleFlowList : FlowListBase
    {
        public MainBattleFlowList()
        {
            FlowList.Add(FlowDefine.FlowType.Game);
        }
    }
}
