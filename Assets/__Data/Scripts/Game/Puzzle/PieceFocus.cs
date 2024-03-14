using System;
using System.Collections;
using System.Collections.Generic;
using MonLab;
using UnityEngine;

namespace CSG.Puzzle
{
    public class PieceFocus : MonoBehaviour
    {
        private GameObject _selectObj;
        private VrButton _vrButton;

        public Action<PieceFocus> onFocusAction;

        public int index;

        private void OnFocus()
        {
            onFocusAction?.Invoke(this);
        }

        private void Awake()
        {
            _selectObj = transform.GetChild(0).gameObject;
            _vrButton = transform.GetChild(1).GetComponent<VrButton>();
            _vrButton.Btn.onClick.AddListener(OnFocus);
        }

        private void OnEnable()
        {
            _selectObj.SetActive(false);
        }

        private void OnDisable()
        {
            _selectObj.SetActive(true);
        }

        public void On(bool isResize)
        {
            if (isResize)
            {
                transform.localScale = Vector3.one * 0.85f;
            }
            
            _selectObj.SetActive(true);
        }

        public void Off(bool isResize)
        {
            if (isResize)
            {
                transform.localScale = Vector3.one * 0.80f;

            }

            _selectObj.SetActive(false);
        }
    }
}
