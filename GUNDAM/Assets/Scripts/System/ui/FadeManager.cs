using app.Battle;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace app
{
    [DefaultExecutionOrder((int)ExecutionOrder.SYSTEM)]
    public class FadeManager : SingletonBehaviour<FadeManager>
    {
        public GameObject FadeUI;

        private Image FadeUIImage = null;

        private float AlphaRate = 0;

        public void FadeIn(float time)
        {
            if ( FadeUIImage != null)
            { 
                FadeUIImage.CrossFadeAlpha(0, time, true);
            }
        }
        public void FadeOut(float time)
        {
            if (FadeUIImage != null)
            {
                FadeUIImage.CrossFadeAlpha(1, time, true);
            }
        }
        #region Behaviour継承

        protected override void doStart()
        {
            if (FadeUI != null)
            {
                FadeUIImage =  FadeUI.GetComponent<Image>();
                FadeUIImage.CrossFadeAlpha(0, 0, true);
            }
        }

        //protected override void doStart()
        //{
        //
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