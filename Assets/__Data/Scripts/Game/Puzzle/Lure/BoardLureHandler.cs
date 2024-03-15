using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSG.Puzzle
{
    public class BoardLureHandler : LureHandler
    {
        public const float LureOnceDuration = 0.5f;

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

        protected override void LureListInit()
        {
            _lureList = new List<Lure>();

            foreach (Transform t in transform)
            {
                Lure lure = t.gameObject.GetComponent<Lure>();

                _lureList.Add(lure);
            }
        }

        protected override void ValueInit()
        {
            
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            foreach (Lure lure in _lureList)
            {
                lure.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (ReferenceEquals(PieceFocusHandler.CurrentFocus, null))
            {
                _timer = 0.0f;
            }
        }

        protected override void OnLure()
        {
            foreach (Lure lure in _lureList)
            {
                if (lure.IsCanLure)
                {
                    lure.Play();
                }
            }

            _timer = 0.0f;
            _isLureWait = false;
        }
    }
}
