using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app
{
    public abstract class LoadBehavior : MonoBehaviour
    {
        public abstract bool IsLoading();
        public abstract void OnLoadFinish();

        public bool IsReady { get; private set; }
    }
}
