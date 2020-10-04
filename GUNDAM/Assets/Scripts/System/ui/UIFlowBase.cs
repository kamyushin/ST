using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app.UI
{
    public class UIFlowBase : MonoBehaviour
    {
        protected UIController Controller = null;

        // Start is called before the first frame update
        void Start()
        {
            Controller = GetComponent<UIController>();
        }
    }
}
