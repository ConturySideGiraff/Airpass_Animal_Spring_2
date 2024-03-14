using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSG.Puzzle
{
    public class PieceFocusHandler : MonoBehaviour
    {
        [SerializeField]
        private List<PieceFocus> _flowerList = new List<PieceFocus>();
        [SerializeField]
        private List<PieceFocus> _animalList = new List<PieceFocus>();

        private void Awake()
        {
            foreach (Transform t in transform.GetChild(0))
            {
                PieceFocus focus = t.gameObject.GetComponent<PieceFocus>();
                focus.onFocusAction = OnFocus;
                _flowerList.Add(focus);
            }

            foreach (Transform t in transform.GetChild(1))
            {
                PieceFocus focus = t.gameObject.GetComponent<PieceFocus>();
                focus.onFocusAction = OnFocus;
                _animalList.Add(focus);
            }
        }

        private void OnDisable()
        {
            if (ReferenceEquals(CurrentFocus, null))
            {
                return;
            }
            
            CurrentFocus.Off();
            CurrentFocus = null;
        }

        public static PieceFocus CurrentFocus { get; set; }

        private void OnFocus(PieceFocus focus)
        {
            if (ReferenceEquals(CurrentFocus, null))
            {
                CurrentFocus = focus;
                CurrentFocus.On();
            }

            else
            {
                if (CurrentFocus != focus)
                {
                    return;
                }

                CurrentFocus.Off();
                CurrentFocus = null;
            }
        }

        public void Set(Species species, BoardRuntimeData runtimeData)
        {
            switch (species)
            {
                case Species.Animal:
                    for (var i = 0; i < _flowerList.Count; i++)
                    {
                        _flowerList[i].gameObject.SetActive(false);
                    }

                    for (var i = 0; i < _animalList.Count; i++)
                    {
                        _animalList[i].index = i;
                        _animalList[i].gameObject.SetActive(true);
                    }

                    break;
                case Species.Flower:
                    for (var i = 0; i < _flowerList.Count; i++)
                    {
                        _flowerList[i].index = i;
                        _flowerList[i].gameObject.SetActive(true);
                    }

                    for (var i = 0; i < _animalList.Count; i++)
                    {
                        _animalList[i].gameObject.SetActive(false);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(species), species, null);
            }
        }
    }
}
