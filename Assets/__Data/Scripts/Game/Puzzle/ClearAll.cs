using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSG.Puzzle
{
    public class ClearAll : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _dimmdCg;
        [SerializeField] private UIAnimFlipBook _sucessFlipBook;
        [SerializeField] private UIAnimFlipBook _cherryBlossomFlipBook;
        [SerializeField] private UIAnimFlipBook _magnoliaFlipBook;
        [SerializeField] private UIAnimFlipBook _butterflyFlipBook;

        [Space] 
        [SerializeField] private GameObject _butterflayObj;
        
        private void Awake()
        {
            _dimmdCg.gameObject.SetActive(false);
            _sucessFlipBook.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            if(!ReferenceEquals(_waitCo, null)) StopCoroutine(_waitCo);
            _waitCo = null;
            
            _dimmdCg.gameObject.SetActive(false);
            _sucessFlipBook.gameObject.SetActive(false);
            _cherryBlossomFlipBook.gameObject.SetActive(false);
            _magnoliaFlipBook.gameObject.SetActive(false);
            _butterflyFlipBook.gameObject.SetActive(false);
        }

        public void Wait(float duration)
        {
            if(!ReferenceEquals(_waitCo, null)) StopCoroutine(_waitCo);
            _waitCo = WaitCo(duration);
            StartCoroutine(_waitCo);
        }

        public void OtherFlipStart()
        {
            _cherryBlossomFlipBook.AnimStop();
            _cherryBlossomFlipBook.gameObject.SetActive(true);
            
            _magnoliaFlipBook.AnimStop();
            _magnoliaFlipBook.gameObject.SetActive(true);
            
            _butterflayObj.SetActive(false);
            _butterflyFlipBook.AnimStop();
            _butterflyFlipBook.gameObject.SetActive(true);
            _cherryBlossomFlipBook.AnimStart();
            _magnoliaFlipBook.AnimStart();
            _butterflyFlipBook.AnimStart();
        }

        private IEnumerator _waitCo;
        
        private IEnumerator WaitCo(float duration)
        {
            _dimmdCg.alpha = 0.0f;
            _dimmdCg.gameObject.SetActive(true);
            
            _sucessFlipBook.AnimStop();
            _sucessFlipBook.gameObject.SetActive(true);
            
            _sucessFlipBook.AnimStart();

            // _cherryBlossomFlipBook.AnimStop();
            // _cherryBlossomFlipBook.gameObject.SetActive(true);
            //
            // _magnoliaFlipBook.AnimStop();
            // _magnoliaFlipBook.gameObject.SetActive(true);
            //
            // _butterflyFlipBook.AnimStop();
            // _butterflyFlipBook.gameObject.SetActive(true);
            
            // _cherryBlossomFlipBook.AnimStart();
            // _magnoliaFlipBook.AnimStart();
            // _butterflyFlipBook.AnimStart();
            
            for(float t = 0.0f; t<= duration; t+= TimerManager.Instance.Reduce)
            {
                float alphaPercent = Mathf.Clamp(t / 1.0f, 0.0f, 1.0f);

                _dimmdCg.alpha = alphaPercent;

                yield return null;
            }
            
            GameManager.Instance.ToResult(true);
        }
    }
}
