using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSG.Puzzle
{
    public class ClearOnce : MonoBehaviour
    {
        public BoardSelect _boardSelect;

        public void Correct()
        {
            _boardSelect.gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}
