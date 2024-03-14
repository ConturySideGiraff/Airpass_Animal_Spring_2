using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSG.Puzzle
{
    public class BoardLure : Lure
    {
        public override bool IsCanLure { get; }
        
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
            
            transform.GetChild(0).gameObject.SetActive(false);
        }
        
        private IEnumerator _coLure;

        private IEnumerator CoLure()
        {
            yield return null;
        }
    }
}
