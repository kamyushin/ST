using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace app
{
    public class FlowManager : SingletonBehaviour<FlowManager>
    {
        public void RequestLoad(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public void RequestUnload(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
            Resources.UnloadUnusedAssets();
        }

        #region Behaviour継承

        //protected override void doAwake()
        //{
        //
        //}

        //protected override void doStart()
        //{
        //
        //}

        //protected override void doUpdate()
        //{
        //
        //}

        #endregion

        #region 継承禁止
        // 使用禁止。doAwake継承
        void Awake()
        {
        }

        // 使用禁止。doStart継承
        void Start()
        {
        }

        // 使用禁止。doUpdate継承
        void Update()
        {
        }
        #endregion
    }
}