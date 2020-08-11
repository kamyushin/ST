using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace app
{
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
                    CurrentPhase.Start();
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

        private void Awake()
        {
            RegistPhase();

            Phase.Setup();
        }

        private void Update()
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
        }

        protected virtual void RegistPhase() { }
    }
}
