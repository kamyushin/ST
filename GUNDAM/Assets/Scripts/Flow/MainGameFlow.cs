using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
    public class MainGameFlow : FlowBase
    {
        protected override void RegistPhase()
        {
            Phase.RegistPhase(UpdateGame);
        }

        private FlowState UpdateGame()
        {
            return FlowState.CONTINUE;
        }
    }
}
