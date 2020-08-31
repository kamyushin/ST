using app.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
    public class MainGameFlow : FlowBase
    {
        protected override void RegistPhase()
        {
            Phase.RegistPhase(UpdateGame,SetupBattle);
        }

        private void SetupBattle()
        {
            if (BattleManager.IsInstanceEnable)
            {
                BattleManager.Instance.SetUpBattle();
            }
        }
        
        private FlowState UpdateGame()
        {
            if (BattleManager.IsInstanceEnable)
            {
                if (BattleManager.Instance.IsBattleFinish())
                {
                    return FlowState.END;
                }
            }
            return FlowState.CONTINUE;
        }
    }
}
