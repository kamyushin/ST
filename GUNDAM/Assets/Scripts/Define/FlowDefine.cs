using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
    public class FlowDefine : MonoBehaviour
    {
        public static string BootSceneName = "Boot";
        public static string ResidentSceneName = "Resident";

        public enum FlowType
        {
            Title,
            Game,
        }

        public static Dictionary<FlowType, string[]> LoadSceneNames = new Dictionary<FlowType, string[]>
        {
            { FlowType.Title,new string[]{"Title" } },
            { FlowType.Game,new string[]{"Game" } },
        };
    }
}
