using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace app.UI
{
    public class UIController : MonoBehaviour
    {
        private Animator Animator = null;


        private void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        public void SetState(string paramName)
        {
            if (Animator == null) return;

            this.Animator.SetTrigger(paramName);
        }
    }
}
