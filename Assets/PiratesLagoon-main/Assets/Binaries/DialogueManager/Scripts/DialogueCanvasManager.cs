using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI_Manager
{
    public class DialogueCanvasManager : MonoBehaviour
    {
        private Animator _animator;

        private void Start() {
            _animator = GetComponent<Animator>();
        }

        public void SetButtonsNeedUpdate(bool b)
        {
            _animator.SetBool("ButtonsNeedUpdate", b);
        }

        public void ButtonsDontNeedUpdate()
        {
            _animator.SetBool("ButtonsNeedUpdate", false);
        }
    }
}
