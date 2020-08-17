using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
    public class TitleFlow : FlowBase
    {
        protected override FlowDefine.GameFlowType FlowType { get { return FlowDefine.GameFlowType.Title; } }
        protected override void RegistPhase()
        {
            base.RegistPhase();

            Phase.RegistPhase(UpdateTitle);
        }

        private FlowState UpdateTitle()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RequestFlowStart(FlowDefine.GameFlowType.Game);
                return FlowState.END;
            }

            return FlowState.CONTINUE;
        }
    }
}
