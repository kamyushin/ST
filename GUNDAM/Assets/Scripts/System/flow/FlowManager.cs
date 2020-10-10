using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace app
{
    public class FlowManager : SingletonBehaviour<FlowManager>
    {
        public FlowMap FlowMap { get; private set; } = null;

        public void RequestFlowMap(FlowDefine.FlowMapType type)
        {
            switch (type)
            {
                case FlowDefine.FlowMapType.Title:
                    FlowMap = new TitleFlowMap();
                    FlowMap.StartFlowMap();
                    break;
            }
        }

        public void RequestLoad(FlowDefine.FlowType type)
        {
            string[] loadSceneNames = null;
            FlowDefine.LoadSceneNames.TryGetValue(type, out loadSceneNames);

            foreach(var sceneName in loadSceneNames)
            {
                RequestLoad(sceneName);
            }
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

        #region　非公開メソッド
        private void RequestLoad(string sceneName)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            operation.allowSceneActivation = true;
        }

        private void RequestUnload(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
            Resources.UnloadUnusedAssets();
        }
        #endregion

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