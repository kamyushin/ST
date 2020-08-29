using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace app
{
    public class FlowBoot : MonoBehaviour
    {
        void Awake()
        {
            var loadOperation = SceneManager.LoadSceneAsync(FlowDefine.ResidentSceneName,LoadSceneMode.Additive);
            loadOperation.completed += OnResidentLoad;
        }

        private void OnResidentLoad(UnityEngine.AsyncOperation operation)
        {
            if (FlowManager.IsInstanceEnable)
            {
                FlowManager.Instance.RequestLoad(FlowDefine.GameFlowType.Title);
                SceneManager.UnloadSceneAsync(FlowDefine.BootSceneName);
                Resources.UnloadUnusedAssets();
            }
        }
    }
}
