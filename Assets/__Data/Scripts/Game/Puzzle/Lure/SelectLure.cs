using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSG.Puzzle
{
    public class SelectLure : Lure
    {
        public override bool IsCanLure => gameObject.activeInHierarchy;
        
        public override void Play()
        {
            IsLure = true;
            
            _coLure = CoLure();
            StartCoroutine(_coLure);
        }

        public override void Stop()
        {
            IsLure = false;

            if(!ReferenceEquals(_coLure, null)) StopCoroutine(_coLure);
            _coLure = null;
            
            transform.GetChild(0).gameObject.SetActive(true);
        }
        
        private IEnumerator _coLure;

        private IEnumerator CoLure()
        {
            var o = transform.GetChild(0).gameObject;
            
            while (true)
            {
                for (float t = 0.0f; t <= SelectLureHandler.LureOnceDuration; t += TimerManager.Instance.Reduce)
                {
                    yield return null;
                }
                
                o.SetActive(!o.activeInHierarchy);
            }
        }
    }
}
