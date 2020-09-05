using app;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
    public class MainBattleFlowList : FlowListBase
    {
        protected override void SetFlowList()
        {
            FlowList.Add(FlowDefine.FlowType.Title);
            FlowList.Add(FlowDefine.FlowType.Game);
        }
    }
}
