using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
    public class FlowDefine : MonoBehaviour
    {
        public static string BootSceneName = "Boot";
        public static string ResidentSceneName = "Resident";

        public enum GameFlowType
        {
            Title,
            Game,
        }

        public static Dictionary<GameFlowType, string[]> LoadSceneNames = new Dictionary<GameFlowType, string[]>
        {
            { GameFlowType.Title,new string[]{"Title" } },
            { GameFlowType.Game,new string[]{"Game" } },
        };
    }
}
