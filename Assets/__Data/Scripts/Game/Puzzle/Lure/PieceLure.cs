using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSG.Puzzle
{
    public class PieceLure : Lure
    {
        private PieceMove _move;

        private PieceMove Move => _move ? _move : _move = GetComponent<PieceMove>();

        public override bool IsCanLure => !Move.isCorrect;
        
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

           transform.localScale = Vector3.one * 0.8f;
        }

        private IEnumerator _coLure;

        private IEnumerator CoLure()
        {
            while (true)
            {
                for (float t = 0.0f; t <= PieceLureHandler.LureOnceDuration; t += TimerManager.Instance.Reduce)
                {
                    float percent = t / PieceLureHandler.LureOnceDuration;
                    float size = 0.8f + 0.1f * Mathf.Cos(Mathf.Cos(Mathf.PI * percent));
                    
                    transform.localScale = Vector3.one * size;
                    
                    yield return null;
                }
            }
        }
    }
}
