using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSG.Puzzle
{
    public class BoardFailPanel : MonoBehaviour
    {
        private void OnEnable()
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        public void Active(bool isActive)
        {
            transform.GetChild(0).gameObject.SetActive(isActive);
        }
    }
}
