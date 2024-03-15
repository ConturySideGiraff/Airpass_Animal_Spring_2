using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSG.Puzzle
{
    public class PieceLureHandler : LureHandler
    {
        public const float LureOnceDuration = 1.0f;
        
        protected override void LureListInit()
        {
            _lureList = new List<Lure>();

            foreach (Transform t in transform)
            {
                Lure l = t.gameObject.GetComponent<Lure>();
                
                _lureList.Add(l);
            }
        }

        protected override void ValueInit()
        {
           
        }
        
        

        private void Update()
        {
            if (!ReferenceEquals(PieceFocusHandler.CurrentFocus, null))
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
