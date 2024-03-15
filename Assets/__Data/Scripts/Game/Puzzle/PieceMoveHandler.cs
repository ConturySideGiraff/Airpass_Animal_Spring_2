using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CSG.Puzzle
{
    public class PieceMoveHandler : MonoBehaviour
    {
        [SerializeField] private BoardPointStart _pointStart;
        [SerializeField] private BoardPointEnd _pointEnd;
        [Space]
        
        [SerializeField]
        private List<PieceMove> _moveFlowerList = new List<PieceMove>();
        private List<Image> _imgFlowerList = new List<Image>();

        
        [SerializeField]
        private List<PieceMove> _moveAnimalList = new List<PieceMove>();
        private List<Image> _imgAnimalList = new List<Image>();

        private void Awake()
        {
            foreach (Transform t in transform.GetChild(0))
            {
                _moveFlowerList.Add(t.gameObject.GetComponent<PieceMove>());
                _imgFlowerList.Add(t.GetChild(1).gameObject.GetComponent<Image>());
            }
            
            foreach (Transform t in transform.GetChild(1))
            {
                _moveAnimalList.Add(t.gameObject.GetComponent<PieceMove>());
                _imgAnimalList.Add(t.GetChild(1).gameObject.GetComponent<Image>());
            }
        }

        public bool IsCanQuit
        {
            get
            {
                if (transform.GetChild(0).gameObject.activeInHierarchy)
                {
                    foreach (PieceMove move in _moveFlowerList)
                    {
                        if (move.IsMoving)
                        {
                            return false;
                        }
                    }

                    return true;
                }

                else
                {
                    foreach (PieceMove move in _moveAnimalList)
                    {
                        if (move.IsMoving)
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }
        }

        public void Set(Species species, List<Sprite> colorSpriteList, BoardRuntimeData runtimeData)
        {
            switch (species)
            {
                case Species.Animal:
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(true);
                    
                    for (var i = 0; i < colorSpriteList.Count; i++)
                    {
                        int runtimeIndex = runtimeData.startPointIndexArr[i];

                        if (runtimeData.correctIndexArr[runtimeIndex])
                        {
                            _moveAnimalList[runtimeIndex].transform.position = _pointEnd.AnimalPointList[runtimeIndex].position;
                            _moveAnimalList[runtimeIndex].transform.localScale = Vector3.one;
                            _moveAnimalList[runtimeIndex].isCorrect = true;
                        }

                        else
                        {
                            _moveAnimalList[runtimeIndex].startPosition = _pointStart.AnimalPointList[i].position;
                            _moveAnimalList[runtimeIndex].targetTransform = _pointEnd.AnimalPointList[runtimeIndex];
                            _moveAnimalList[runtimeIndex].transform.position = _pointStart.AnimalPointList[i].position;
                            _moveAnimalList[runtimeIndex].transform.localScale = Vector3.one * 0.8f;
                            _moveAnimalList[runtimeIndex].isCorrect = false;


                      
                        }
                              
                        _imgAnimalList[i].sprite = colorSpriteList[i];
                        _imgAnimalList[i].SetNativeSize();
                        
                        _moveAnimalList[runtimeIndex].index = runtimeIndex;
                        
              
                    }
                    
                    break;
                
                case Species.Flower:
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(1).gameObject.SetActive(false);
                    
                    for (var i = 0; i < colorSpriteList.Count; i++)
                    {
                        int runtimeIndex = runtimeData.startPointIndexArr[i];

                        if (runtimeData.correctIndexArr[runtimeIndex])
                        {
                            _moveFlowerList[runtimeIndex].transform.position = _pointEnd.FlowerPointList[runtimeIndex].position;
                            _moveFlowerList[runtimeIndex].transform.localScale = Vector3.one;
                            _moveFlowerList[runtimeIndex].isCorrect = true;
                        }

                        else
                        {
                            _moveFlowerList[runtimeIndex].startPosition = _pointStart.FlowerPointList[i].position;
                            _moveFlowerList[runtimeIndex].targetTransform = _pointEnd.FlowerPointList[runtimeIndex];
                            _moveFlowerList[runtimeIndex].transform.position = _pointStart.FlowerPointList[i].position;
                            _moveFlowerList[runtimeIndex].transform.localScale = Vector3.one * 0.8f;
                            _moveFlowerList[runtimeIndex].isCorrect = false;
                        }
                        
                        _imgFlowerList[i].sprite = colorSpriteList[i];
                        _imgFlowerList[i].SetNativeSize();
                        
                        _moveFlowerList[runtimeIndex].index = runtimeIndex;
                    }
                    break;
            }
        }

        public PieceMove Get(int index)
        {
            if (transform.GetChild(0).gameObject.activeInHierarchy)
            {
                return _moveFlowerList[index];
            }

            else
            {
                return _moveAnimalList[index];
            }
        }
    }
}
