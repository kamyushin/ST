using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app.UI
{
    public abstract class UIFlowBase : MonoBehaviour
    {
        protected UIController Controller = null;

        public UIHandle Handle = null;


        // Start is called before the first frame update
        void Start()
        {
            Controller = GetComponent<UIController>();
        }

        public void StartFlow(UIHandle handle)
        {
            Handle = handle;
        }
    }
}
