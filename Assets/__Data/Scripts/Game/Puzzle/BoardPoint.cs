using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSG.Puzzle
{
    public abstract class BoardPoint : MonoBehaviour
    {
        [SerializeField]
        protected List<Transform> _flowerPointList = new List<Transform>();
        [SerializeField]
        protected List<Transform> _animalPointList = new List<Transform>();

        public List<Transform> FlowerPointList => _flowerPointList;

        public List<Transform> AnimalPointList => _animalPointList;
        
        protected virtual void Awake()
        {
            foreach (Transform t in transform.GetChild(0))
            {
                _flowerPointList.Add(t);
            }

            foreach (Transform t in transform.GetChild(1))
            {
                _animalPointList.Add(t);
            }
        }
    }
}
