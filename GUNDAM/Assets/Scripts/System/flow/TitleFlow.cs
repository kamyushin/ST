using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
    public class TitleFlow : FlowBase
    {
        protected override FlowDefine.FlowType FlowType { get { return FlowDefine.FlowType.Title; } }
        protected override void RegistPhase()
        {
            base.RegistPhase();

            Phase.RegistPhase(UpdateTitle);
        }

        private FlowState UpdateTitle()
        {
            if (Input.anyKeyDown)
            {
                if (FlowManager.IsInstanceEnable)
                {
                    RequestFlowStart(FlowDefine.FlowType.Game);
                }
                return FlowState.END;
            }

            return FlowState.CONTINUE;
        }
    }
}
