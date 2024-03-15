using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSG.Puzzle
{
    public class ClearEffectHandler : MonoBehaviour
    {
        [SerializeField]
        private List<ClearOnce> _clearOnceList = new List<ClearOnce>();

        [SerializeField]
        private ClearAll _clearAll;
        
        private void Awake()
        {
            foreach (Transform t in transform.GetChild(0))
            {
                _clearOnceList.Add(t.gameObject.GetComponent<ClearOnce>());
            }
        }

        private void OnEnable()
        {
            foreach (ClearOnce o in _clearOnceList)
            {
                o.gameObject.SetActive(false);
            }
        }

        public void BoardOnceClear(bool isAllClear)
        {
            int index = BoardHandler.CurrentIndex;
            
            _clearOnceList[index].Correct();

            var fpPrefab = Resources.Load<UIAnimFlipBook>("BoardOnceClear");

            var fp =  UIAnimFlipBook.Instantiate(fpPrefab, transform);

            fp.transform.position = _clearOnceList[index].transform.position;
            fp.AnimStart();
            
            AudioManager.Instance.SfxPlay(AudioClipName.BoardOnceClear);
            
            Destroy(fp.gameObject, 1.0f);

            if (isAllClear)
            {
                TimerManager.Instance.GameTimerStop();
                
                StartCoroutine(BoardAllClear());
            }
        }

        private IEnumerator BoardAllClear()
        {
            _clearAll.OtherFlipStart();
            
            for (float t = 0.0f; t <= 3.0f; t += TimerManager.Instance.Reduce)
            {
                yield return null;
            }
            
            AudioManager.Instance.SfxPlay(AudioClipName.GameAllClear);
            
            _clearAll.Wait(5.0f);
        }
    }
}
