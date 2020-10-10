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
        private float DecideTimer = 1.0f;

        protected override void RegistPhase()
        {
            base.RegistPhase();

            OnFlowStart += StartFlow;
            OnFlowEnd += EndFlow;

            Phase.RegistPhase(UpdateTitle);
        }

        private void StartFlow()
        {
            TitleHandle = UIFlowTitle.StartFlow();
        }
        private void EndFlow()
        {
            TitleHandle.End = true;
        }

        private FlowState UpdateTitle()
        {
            if (UIFlowTitle.IsDecide)
            {
                DecideTimer -= Time.deltaTime;
                if (DecideTimer <= 0)
                {
                    if (FlowManager.IsInstanceEnable)
                    {
                        RequestFlowStart(FlowDefine.FlowType.Game);
                    }
                    return FlowState.END;
                }
            }

            return FlowState.CONTINUE;
        }
    }
}
