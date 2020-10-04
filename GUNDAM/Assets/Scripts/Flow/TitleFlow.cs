using app.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
    public class TitleFlow : FlowBase
    {
        protected override FlowDefine.FlowType FlowType { get { return FlowDefine.FlowType.Title; } }

        private UIHandle TitleHandle = null;
        protected override void RegistPhase()
        {
            base.RegistPhase();

            Phase.RegistPhase(UpdateTitle,StartTitle);
        }

        private void StartTitle()
        {
            TitleHandle = UIFlowTitle.StartFlow();
        }

        private FlowState UpdateTitle()
        {
            if (TitleHandle.End)
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
