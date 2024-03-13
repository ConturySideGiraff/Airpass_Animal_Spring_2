using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimCrossMove : UIAnim
{
    private enum Dir
    {
        Vertical,
        Horizontal,
    }

    [SerializeField] private Dir _dir = Dir.Vertical;
    [SerializeField] private bool _isLoop = true;
    [SerializeField] private float _delay = 0.0f;
    [SerializeField] private float _duration = 5.0f;
    [SerializeField] private float _distance = 10.0f;
    [SerializeField] private float _speed = 10.0f;
    
    
    protected override IEnumerator CoAnim()
    {
        for (float t = 0.0f; t <= _delay; t += Time.deltaTime)
        {
            yield return null;
        }


        Vector2 dir = _dir == Dir.Horizontal ? Vector2.up : Vector2.down;
        
        float timer = 0.0f;
        float angle = 0.0f;

        if (_isLoop)
        {
            while (true)
            {
                _rectTransform.anchoredPosition = _startVector2 + _distance * dir * angle;
                
                timer += Time.deltaTime * _speed;
                angle = Mathf.Sin(timer * Mathf.Deg2Rad);
                yield return null;
            }
        }

        for (float t = 0.0f; t <= _duration; t += Time.deltaTime)
        {
            _rectTransform.anchoredPosition = _startVector2 + dir * angle;
                
            timer += Time.deltaTime * _speed;
            angle = Mathf.Sin(timer * Mathf.Deg2Rad);
        }
    }
}
