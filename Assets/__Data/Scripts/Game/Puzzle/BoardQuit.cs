using System;
using System.Collections;
using System.Collections.Generic;
using MonLab;
using Unity.VisualScripting;
using UnityEngine;

namespace CSG.Puzzle
{
    public class BoardQuit : MonoBehaviour
    {
        [SerializeField] private PieceMoveHandler _pieceMoveHandler;

        private VrButton _vrBtn;

        private VrButton VrBtn => _vrBtn ? _vrBtn : _vrBtn = GetComponent<VrButton>();

        private void Awake()
        {
            VrBtn.Btn.onClick.AddListener(Quit);
        }

        private void Quit()
        {
            if (!_pieceMoveHandler.IsCanQuit)
            {
                return;
            }
            
            if(BoardPointTouchHandler.isOnBoardCorrectDelay)
            {
                return;
            }

            transform.parent.gameObject.SetActive(false);
        }
    }
}
