using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace app.UI
{
    [DefaultExecutionOrder((int)ExecutionOrder.SYSTEM)]
    public class FadeManager : SingletonBehaviour<FadeManager>
    {
        public GameObject FadeUI;

        private Image FadeUIImage = null;

        private float AlphaRate = 0;

        public float FadeTimer { get; private set; }
        public bool IsFading { get; private set; }

        public void FadeIn(float time = 1.0f)
        {
            if (FadeUIImage != null)
            {
                IsFading = true;
                FadeUIImage.CrossFadeAlpha(0, time, true);
                FadeTimer = time;
            }
        }
        public void FadeOut(float time = 1.0f)
        {
            if (FadeUIImage != null)
            {
                IsFading = true;
                FadeUIImage.CrossFadeAlpha(1, time, true);
                FadeTimer = time;
            }
        }

        #region Behaviour継承

        protected override void doStart()
        {
            if (FadeUI != null)
            {
                FadeUIImage = FadeUI.GetComponent<Image>();
                FadeUIImage.CrossFadeAlpha(0, 0, true);
            }
        }

        //protected override void doStart()
        //{
        //
        //}

        protected override void doUpdate()
        {
            if(IsFading)
            {
                if ( FadeTimer > 0)
                {
                    FadeTimer -= Time.deltaTime;
                }

                if ( FadeTimer <= 0)
                {
                    IsFading = false;
                }
            }
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