using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app.UI
{
    [DefaultExecutionOrder((int)ExecutionOrder.SYSTEM)]
    public class UIFlowManager : SingletonBehaviour<UIFlowManager>
    {
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