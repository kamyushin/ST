using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static bool IsEnableInstance { get { return Instance != null; } }

    #region Behaviour群

    private void Awake()
    {
        CheckInstance();

        doAwake();
    }

    // Start is called before the first frame update
    private void Start()
    {
        doStart();
    }

    // Update is called once per frame
    private void Update()
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
