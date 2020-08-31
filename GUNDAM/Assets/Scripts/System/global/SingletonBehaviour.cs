using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
    [DefaultExecutionOrder((int)ExecutionOrder.SYSTEM)]
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    Type t = typeof(T);

                    instance = (T)FindObjectOfType(t);
                    if (instance == null)
                    {
                        Debug.LogError(t + " をアタッチしているGameObjectはありません");
                    }
                }

                return instance;
            }
        }

        public static bool IsInstanceEnable { get { return Instance != null; } }

        #region Behaviour群

        protected virtual void Awake()
        {
            CheckInstance();

            doAwake();
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            doStart();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            doUpdate();
        }

        protected virtual void doAwake() { }
        protected virtual void doStart() { }
        protected virtual void doUpdate() { }
        #endregion

        protected bool CheckInstance()
        {
            if (instance == null)
            {
                instance = this as T;
                return true;
            }
            else if (Instance == this)
            {
                return true;
            }

            Destroy(this);
            return false;
        }
    }
}
