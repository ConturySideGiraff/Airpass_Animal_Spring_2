using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSG.Puzzle
{
    public class BoardSelectHandler : MonoBehaviour
    {

        [SerializeField] private BoardHandler _boardHandler;

        private List<BoardSelect> _boardSelectList = new List<BoardSelect>();

        private void Awake()
        {
            foreach (Transform t in transform)
            {
                _boardSelectList.Add(t.gameObject.GetComponent<BoardSelect>());
            }

            for (int i = 0; i < _boardSelectList.Count; i++)
            {
                int idx = i;
                _boardSelectList[i].VrBtn.Btn.onClick.AddListener(() => OnSelect(idx));
            }
        }

        private void OnSelect(int index)
        {
            _boardHandler.OnSelect(index);
        }
    }
}
