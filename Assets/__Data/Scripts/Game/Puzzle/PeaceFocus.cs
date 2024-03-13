using System;
using System.Collections;
using System.Collections.Generic;
using MonLab;
using UnityEngine;

namespace CSG.Puzzle
{
    public class PeaceFocus : MonoBehaviour
    {
        private GameObject _selectObj;
        private VrButton _vrButton;

        private void Awake()
        {
            _selectObj = transform.GetChild(0).gameObject;
            _vrButton = transform.GetChild(1).GetComponent<VrButton>();
        }

        private void OnEnable()
        {
            _selectObj.SetActive(false);
        }

        private void OnDisable()
        {
    
        }
    }
}
