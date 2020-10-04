using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
    [DefaultExecutionOrder((int)ExecutionOrder.SYSTEM)]
    public class ResourceLoadManager : SingletonBehaviour<ResourceLoadManager>
    {
        private const string ResourceParentFolderPath = "Assets/Resources/";

        private delegate void CallbackFunc(object loadObject);

        public enum ResourceType
        {
            Chara
        }

        public string[] ResourceChildFolderPaths = new string[]
        {
            "Chara/"
        };

        public void LoadRequest(ResourceType type,string assetName)
        {
            
            var resourceRequest = Resources.LoadAsync(ResourceParentFolderPath + ResourceChildFolderPaths[(int)type] + assetName);
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