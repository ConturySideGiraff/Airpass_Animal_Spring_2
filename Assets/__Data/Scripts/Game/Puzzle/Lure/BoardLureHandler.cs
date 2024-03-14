using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSG.Puzzle
{
    public class BoardLureHandler : LureHandler
    {
        public const float LureOnceDuration = 0.5f;

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
