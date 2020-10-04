using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace app
{
    [DefaultExecutionOrder((int)ExecutionOrder.DEFAULT)]
    public class FlowBase : MonoBehaviour
    {
        public enum FlowState
        {
            CONTINUE,
            NEXT,
            END,
        }

        public class FlowPhase
        {
            public delegate void PhaseStart();
            public delegate FlowState PhaseUpdate();

            uint CurrentIndex = 0;
            SinglePhase CurrentPhase = null;

            private class SinglePhase
            {
                public bool IsInit = true;
                public PhaseStart Start = null;
                public PhaseUpdate Update = null;
            }

            Dictionary<uint, SinglePhase> PhaseDict = new Dictionary<uint, SinglePhase>();

            public void RegistPhase(PhaseUpdate updatePhase,PhaseStart startPhase = null) 
            {
                PhaseDict.Add((uint)PhaseDict.Count, new SinglePhase() { Start = startPhase, Update = updatePhase });
            }

            public void Setup()
            {
                if (PhaseDict.Count == 0) return;

                CurrentPhase = PhaseDict[CurrentIndex];
            }

            public  FlowState Update()
            {
                if (CurrentPhase == null) return FlowState.END;

                if (CurrentPhase.IsInit)
                {
                    if (CurrentPhase.Start != null)
                    {
                        CurrentPhase.Start();
                    }
                    CurrentPhase.IsInit = false;
                }

                return CurrentPhase.Update();
            }

            public bool Next()
            {
                CurrentIndex++;
                if (CurrentIndex >= PhaseDict.Count) return false;

                CurrentPhase = PhaseDict[CurrentIndex];

                return true;
            }
        }

        protected FlowPhase Phase = new FlowPhase();

        protected virtual FlowDefine.FlowType FlowType { get; set; }
        private List<FlowDefine.FlowType> RequestFlowTypes = new List<FlowDefine.FlowType>();
        private bool EndFlow = false;
        private bool FadeInStart = false;
        private bool FadeOutStart = false;

        private void Awake()
        {
#if UNITY_EDITOR
            //Bootじゃない場合ResidentをLoadしに行く
            CheckResident();
#endif

            RegistPhase();

            Phase.Setup();
        }

        private void Start()
        {
            if (UI.FadeManager.IsInstanceEnable)
            {
                if (!UI.FadeManager.Instance.IsFading)
                {
                    UI.FadeManager.Instance.FadeIn();
                    FadeInStart = true;
                }
            }
        }

        private void Update()
        {
            if (FadeInStart)
            {
                if (UI.FadeManager.IsInstanceEnable)
                {
                    if (UI.FadeManager.Instance.IsFading) return;
                }

                FadeInStart = false;
            }

            if (!EndFlow)
            {
                var flowState = Phase.Update();
                var endFlow = false;

                if (flowState == FlowState.NEXT)
                {
                    endFlow = !Phase.Next();
                }
                else if (flowState == FlowState.END)
                {
                    endFlow = true;
                }
                EndFlow = endFlow;
            }
        }

        private void LateUpdate()
        {
            if (EndFlow)
            {
                if (!FadeOutStart)
                {
                    if (UI.FadeManager.IsInstanceEnable)
                    {
                        if (!UI.FadeManager.Instance.IsFading)
                        {
                            UI.FadeManager.Instance.FadeOut();
                            FadeOutStart = true;
                        }
                    }
                }
                
                if (UI.FadeManager.IsInstanceEnable)
                {
                    if (UI.FadeManager.Instance.IsFading) return;
                }

                FadeOutStart = false;

                if (FlowManager.IsInstanceEnable)
                {
                    FlowManager.Instance.RequestUnload(FlowType);
                }
            }

            lock (RequestFlowTypes)
            {
                foreach (var requestFlowType in RequestFlowTypes)
                {
                    if (FlowManager.IsInstanceEnable)
                    {
                        FlowManager.Instance.RequestLoad(requestFlowType);
                    }
                }
            }
            RequestFlowTypes.Clear();
        }

        protected virtual void RegistPhase() { }

        protected void RequestFlowStart(FlowDefine.FlowType flowType)
        {
            RequestFlowTypes.Add(flowType);
        }

#if UNITY_EDITOR
        private void CheckResident()
        {
            bool loadResident = false;
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene.name == FlowDefine.ResidentSceneName)
                {
                    loadResident = true;
                    break;
                }
            }

            if (!loadResident)
            {
                SceneManager.LoadSceneAsync(FlowDefine.ResidentSceneName, LoadSceneMode.Additive);
            }
        }
#endif
    }
}
