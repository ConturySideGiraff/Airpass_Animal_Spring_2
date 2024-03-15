using System;
using System.Collections;
using System.Collections.Generic;
using MonLab;
using UnityEngine;
using UnityEngine.UI;

namespace CSG.Puzzle
{
    public class BoardSelect : MonoBehaviour
    {
        
        private GameObject _dotedImageObject;

        private VrButton _vrBtn;

        public VrButton VrBtn => _vrBtn ? _vrBtn : GetComponent<VrButton>();
        
        private void Awake()
        {
            _dotedImageObject = transform.GetChild(0).gameObject;
        }

        private void OnEnable()
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
