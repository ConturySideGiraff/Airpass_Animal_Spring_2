using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSG.Puzzle
{
    public class BoardLure : Lure
    {
        public PieceMove _pieceMove;

        public override bool IsCanLure => !_pieceMove.isCorrect;
        
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
            var o = transform.GetChild(0).gameObject;
            
            while (true)
            {
                for (float t = 0.0f; t <= BoardLureHandler.LureOnceDuration; t += TimerManager.Instance.Reduce)
                {
                    yield return null;
                }
                
                o.SetActive(!o.activeInHierarchy);
            }
        }
    }
}
