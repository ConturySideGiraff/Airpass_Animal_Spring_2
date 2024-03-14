using System;
using System.Collections;
using System.Collections.Generic;
using MonLab;
using UnityEngine;
using UnityEngine.UI;

namespace CSG.Puzzle
{
    public class BoardPointTouchHandler : BoardPoint
    {
        [SerializeField] protected PieceMoveHandler _pieceMoveHandler;
        
        private List<VrButton> _flowerButtonList = new List<VrButton>();
        private List<VrButton> _animalButtonList = new List<VrButton>();
        
        protected override void Awake()
        {
            base.Awake();

            foreach (Transform t in _flowerPointList)
            {
                VrButton vrBtn = t.gameObject.GetComponent<VrButton>();
                
                Image img = vrBtn.gameObject.GetComponent<Image>();
                Color color = img.color;
                color.a = 0.0f;
                img.color = color;
                
                _flowerButtonList.Add(vrBtn);
            }
            
            foreach (Transform t in _animalPointList)
            {
                VrButton vrBtn = t.gameObject.GetComponent<VrButton>();

                Image img = vrBtn.gameObject.GetComponent<Image>();
                Color color = img.color;
                color.a = 0.0f;
                img.color = color;
                
                _animalButtonList.Add(vrBtn);
            }

            for (int i = 0; i < _flowerButtonList.Count; i++)
            {
                int index = i;
                
                _flowerButtonList[index].Btn.onClick.AddListener(() => OnDown(index));
            }
            
            
            for (int i = 0; i < _animalPointList.Count; i++)
            {
                int index = i;
                
                _animalButtonList[index].Btn.onClick.AddListener(() => OnDown(index));
            }
        }

        public void Set(Species species)
        {
            switch (species)
            {
                case Species.Animal:
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(true);
                    break;
                case Species.Flower:
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(1).gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(species), species, null);
            }
        }

        private void OnDown(int index)
        {
            PieceFocus currentFocus = PieceFocusHandler.CurrentFocus;

            if (ReferenceEquals(currentFocus, null))
            {
                return;
            }

            if (currentFocus.index == index)
            {
                OnAnswerCorrect();
            }
            else
            {
                OnAnswerFail();
            }
            
            currentFocus.Off();

            PieceFocusHandler.CurrentFocus = null;
        }

        private void OnAnswerCorrect()
        {
            int index = PieceFocusHandler.CurrentFocus.index;

            PieceMove move = _pieceMoveHandler.Get(index);
            
            move.MoveStart();

            BoardHandler.PieceCorrect(index);
        }

        private void OnAnswerFail()
        {
            Debug.Log("fail");
        }
    }
}
