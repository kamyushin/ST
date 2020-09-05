using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace app
{
    public class FlowManager : SingletonBehaviour<FlowManager>
    {
        private FlowListBase CurrentFlowList = null;

        public void RequestLoad(FlowDefine.FlowType type)
        {
            string[] loadSceneNames = null;
            FlowDefine.LoadSceneNames.TryGetValue(type, out loadSceneNames);

            foreach(var sceneName in loadSceneNames)
            {
                RequestLoad(sceneName);
            }
        }

        public void RequestLoad(string sceneName)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            operation.allowSceneActivation = true;
        }

        public void RequestUnload(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
            Resources.UnloadUnusedAssets();
        }

        public void RequestUnload(FlowDefine.FlowType type)
        {
            string[] loadSceneNames = null;
            FlowDefine.LoadSceneNames.TryGetValue(type, out loadSceneNames);

            foreach (var sceneName in loadSceneNames)
            {
                RequestUnload(sceneName);
            }
        }

        #region Behaviour継承

        //protected override void doAwake()
        //{
        //}

        //protected override void doStart()
        //{
        //}

        protected override void doUpdate()
        {
        
        }

        #endregion

        #region 継承禁止
        // 使用禁止。doAwake継承
        protected override void Awake()
        {
            base.Awake();
        }

        // 使用禁止。doStart継承
        protected override void Start()
        {
            base.Start();
        }

        // 使用禁止。doUpdate継承
        protected override void Update()
        {
            base.Update();
        }
        #endregion
    }
}