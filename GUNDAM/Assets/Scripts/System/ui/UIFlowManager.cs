using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app.UI
{
    public class UIHandle
    {
        public bool End { get; set; } = false;
    }


    [DefaultExecutionOrder((int)ExecutionOrder.SYSTEM)]
    public class UIFlowManager : SingletonBehaviour<UIFlowManager>
    {
        public Canvas Canvas = null;

        [System.Serializable]
        public class UIObjectList
        {
            public string Name = "";
            public GameObject Obj = null;
        }

        [SerializeField]
        private List<UIObjectList> UIObjList = new List<UIObjectList>();

        public UIHandle StartFlow(string uiName)
        {
            if (uiName == "") return null;

            var handle = new UIHandle();

            var uiData = UIObjList.Find(x => x.Name == uiName);
            if (uiData != null)
            {
                var uiObj = Instantiate(uiData.Obj,Canvas.gameObject.transform);
                if (uiObj != null)
                {
                    var uiFlow = uiObj.GetComponent<UIFlowBase>();
                    if (uiFlow != null)
                    {
                        uiFlow.Handle = handle;
                    }
                }
            }

            return handle;
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