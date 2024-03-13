using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimDimmedOnly : UIAnim
{
    [SerializeField] private float _delay = 0.0f;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _startAlpha = 1.0f;
    [SerializeField] private float _endAlpha = 0.0f;

    protected override IEnumerator CoAnim()
    {
        _canvasGroup.alpha = _startAlpha;
        
        for (float t = 0.0f; t <= _delay; t += Time.deltaTime)
        {
            yield return null;
        }
        
        for (float t = 0.0f; t <= _duration; t += Time.deltaTime)
        {
            float percent = t / _duration;

            _canvasGroup.alpha = Mathf.Lerp(_startAlpha, _endAlpha, percent);
            
            yield return null;
        }

        _canvasGroup.alpha = _endAlpha;
    }
}
