using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CSG.Puzzle
{
    public class PeaceMoveHandler : MonoBehaviour
    {
        
        [SerializeField] private BoardPointStart _pointStart;
        [Space]
        
        [SerializeField]
        private List<PeaceMove> _moveFlowerList = new List<PeaceMove>();
        private List<Image> _imgFlowerList = new List<Image>();

        
        [SerializeField]
        private List<PeaceMove> _moveAnimalList = new List<PeaceMove>();
        private List<Image> _imgAnimalList = new List<Image>();

        private void Awake()
        {
            foreach (Transform t in transform.GetChild(0))
            {
                _moveFlowerList.Add(t.gameObject.GetComponent<PeaceMove>());
                _imgFlowerList.Add(t.GetChild(1).gameObject.GetComponent<Image>());
            }
            
            foreach (Transform t in transform.GetChild(1))
            {
                _moveAnimalList.Add(t.gameObject.GetComponent<PeaceMove>());
                _imgAnimalList.Add(t.GetChild(1).gameObject.GetComponent<Image>());
            }
        }

        public void Set(Species species, List<Sprite> colorSpriteList)
        {
            switch (species)
            {
                case Species.Animal:
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(true);
                    
                    for (var i = 0; i < colorSpriteList.Count; i++)
                    {
                        _moveAnimalList[i].transform.position = _pointStart.AnimalPointList[i].position;
                        _imgAnimalList[i].sprite = colorSpriteList[i];
                    }
                    
                    break;
                
                case Species.Flower:
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(1).gameObject.SetActive(false);
                    
                    for (var i = 0; i < colorSpriteList.Count; i++)
                    {
                        _moveAnimalList[i].transform.position = _pointStart.FlowerPointList[i].position;
                        _imgFlowerList[i].sprite = colorSpriteList[i];
                    }
                    break;
            }
        }
    }
}
