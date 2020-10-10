using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
    public class TitleFlowMap : FlowMap
    {
        public TitleFlowMap() : base()
        {
            ManageFlowList.Add(FlowDefine.FlowType.Title);
        }
    }
}
