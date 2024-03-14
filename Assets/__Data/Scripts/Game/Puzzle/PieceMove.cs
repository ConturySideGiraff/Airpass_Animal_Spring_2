using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSG.Puzzle
{
    public class PieceMove : MonoBehaviour
    {
        public bool IsMoving; // { get; private set; }
        
        public bool isCorrect;

        public Vector3 startPosition;
        
        public Transform targetTransform;

        
        public int index;

        public void MoveStart()
        {
            if (ReferenceEquals(targetTransform, null))
            {
                return;
            }

            MoveEnd();

            isCorrect = true;
            IsMoving = true;
            
            _coMove = CoMove();
            StartCoroutine(_coMove);
        }

        private void MoveEnd()
        {
            if (!ReferenceEquals(_coMove, null))
            {
                StopCoroutine(_coMove);
            }

            _coMove = null;

            IsMoving = false;
        }

        private void OnDisable()
        {
            MoveEnd();
        }

        private IEnumerator _coMove;

        private IEnumerator CoMove()
        {
            const float duration = 1.0f;

            float startLocalScaleX = transform.localScale.x;
            
            for (float t = 0.0f; t <= duration; t += TimerManager.Instance.Reduce)
            {
                float percent = t / duration;

                transform.position = Vector3.Lerp(startPosition, targetTransform.position, percent);
                transform.localScale = Vector3.one * Mathf.Lerp(startLocalScaleX, 1.0f, percent);
                
                yield return null;
            }

            transform.position = targetTransform.position;
            transform.localScale = Vector3.one;

            IsMoving = false;
            _coMove = null;
        }
    }
}
