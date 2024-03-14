using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSG.Puzzle
{
    public class BoardPieceTouch : MonoBehaviour
    {
        public int index;

        private void Awake()
        {
            index = transform.GetSiblingIndex();
        }
    }
}
