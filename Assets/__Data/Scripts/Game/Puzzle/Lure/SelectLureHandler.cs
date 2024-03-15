using System;
using System.Collections;
using System.Collections.Generic;
using MonLab;
using UnityEngine;

namespace CSG.Puzzle
{
    public class SelectLureHandler : LureHandler
    {
        [SerializeField] private BoardHandler _boardHandler;
        
        public const float LureOnceDuration = 0.5f;

        protected override void LureListInit()
        {
            _lureList = new List<Lure>();

            foreach (Transform t in transform)
            {
                _lureList.Add(t.gameObject.GetComponent<Lure>());
            }
        }

        protected override void ValueInit()
        {
            
        }

        private void Update()
        {
            if (_boardHandler.gameObject.activeInHierarchy)
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
