using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimAlphaScale : UIAnim
{
    [SerializeField] private float _delay = 2.0f;
    [SerializeField] private float _duration = 1.0f;
    [Space]
    [SerializeField] private Vector3 _startScale = Vector3.zero;
    [SerializeField] private Vector3 _endScale= Vector3.one;
    [Space]
    [SerializeField] private float _startAlpha = 0.0f;
    [SerializeField] private float _endAlpha = 1.0f;

    protected override IEnumerator CoAnim()
    {
        transform.localScale = _startScale;
        _canvasGroup.alpha = _startAlpha;
        
        for (float t = 0.0f; t <= _delay; t += Time.deltaTime)
        {
            yield return null;
        }

        for (float t = 0.0f; t <= _duration; t += Time.deltaTime)
        {
            float percent = t / _duration;

            transform.localScale = Vector3.Lerp(_startScale, _endScale, percent);
            _canvasGroup.alpha = Mathf.Lerp(_startAlpha, _endAlpha, percent);
            
            yield return null;
        }

        transform.localScale = _endScale;
        _canvasGroup.alpha = _endAlpha;
    }
}
