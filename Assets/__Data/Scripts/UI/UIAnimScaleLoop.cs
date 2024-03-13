using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimScaleLoop : UIAnim
{
    [SerializeField] private float _minSize = 0.8f;
    [SerializeField] private float _maxSize = 1.0f;
    [SerializeField] private float _oneLoopDuration = 1.0f;
    
    protected override IEnumerator CoAnim()
    {
        float de = _maxSize - _minSize;
        
        while (true)
        {
            for (float t = 0.0f; t <= _oneLoopDuration; t += TimerManager.Instance.Reduce)
            {
                float percent = t / _oneLoopDuration;

                transform.localScale = Vector3.one * (_minSize + de * Mathf.Abs(Mathf.Cos(percent * Mathf.PI)));
                
                yield return null;
            }
        }
    }
}
