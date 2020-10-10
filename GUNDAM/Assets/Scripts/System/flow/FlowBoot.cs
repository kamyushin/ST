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

            loadOperation.allowSceneActivation = true;
            loadOperation.completed += OnResidentLoad;
        }

        private void OnResidentLoad(UnityEngine.AsyncOperation operation)
        {
            if (FlowManager.IsInstanceEnable)
            {
                FlowManager.Instance.RequestFlowMap(FlowDefine.FlowMapType.Title);
                SceneManager.UnloadSceneAsync(FlowDefine.BootSceneName);
                Resources.UnloadUnusedAssets();
            }

            operation.completed -= OnResidentLoad;
        }
    }
}
