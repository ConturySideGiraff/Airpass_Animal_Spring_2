using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CSG.Puzzle
{
    public class BoardPointEnd : BoardPoint
    {
        private List<Image> _flowerImageList = new List<Image>();
        private List<Image> _animalImageList = new List<Image>();
        
        
        
        protected override void Awake()
        {
            base.Awake();

            foreach (Transform t in _flowerPointList)
            {
                _flowerImageList.Add(t.gameObject.GetComponent<Image>());
            }
            
            foreach (Transform t in _animalPointList)
            {
                _animalImageList.Add(t.gameObject.GetComponent<Image>());
            }
        }

        public void BoardImageSet(Species species, List<Sprite> blackSpriteList)
        {
            switch (species)
            {
                case Species.Animal:
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(true);

                    for (var i = 0; i < _animalImageList.Count; i++)
                    {
                        _animalImageList[i].sprite = blackSpriteList[i];
                    }

                    break;
                
                case Species.Flower:
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(1).gameObject.SetActive(false);
                    
                    for (var i = 0; i < _flowerImageList.Count; i++)
                    {
                        _flowerImageList[i].sprite = blackSpriteList[i];
                    }
                    break;
            }
        }
    }
}
