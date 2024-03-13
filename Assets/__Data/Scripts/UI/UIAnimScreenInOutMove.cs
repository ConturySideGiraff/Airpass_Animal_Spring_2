using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIAnimScreenInOutMove : UIAnim
{
    private enum InOut
    {
        InToOut,
        OutToIn
    }
    
    private enum Dir
    {
        Light,
        Right,
        Up,
        Down
    }

    [SerializeField] private InOut _inOut;
    [SerializeField] private Dir _dir;
    [SerializeField] private float _delay = 0.0f;
    [SerializeField] private float _duration = 2.0f;

    protected override IEnumerator CoAnim()
    {
        Vector2 targetVector2;

        if (_inOut == InOut.InToOut)
        {
            targetVector2 = DirToVector;
        }

        else
        {
            targetVector2 = _startVector2;
            _startVector2 = DirToVector;
            _rectTransform.anchoredPosition = _startVector2;
        }

        for (float t = 0.0f; t <= _delay; t += Time.deltaTime)
        {
            yield return null;
        }

        for (float t = 0.0f; t <= _duration; t += Time.deltaTime)
        {
            float percent = t / _duration;

            _rectTransform.anchoredPosition = Vector2.Lerp(_startVector2, targetVector2, percent);
            
            yield return null;
        }

        _rectTransform.anchoredPosition = targetVector2;
    }

    private Vector2 DirToVector => _dir switch
    {
        Dir.Light => new Vector2(-_rectTransform.sizeDelta.x * 0.5f, _rectTransform.anchoredPosition.y),
        Dir.Right => new Vector2(Screen.width + _rectTransform.sizeDelta.x * 0.5f, _rectTransform.anchoredPosition.y),
        Dir.Up => new Vector2(_rectTransform.anchoredPosition.x , _rectTransform.sizeDelta.y),
        Dir.Down => new Vector2(_rectTransform.anchoredPosition.x , _rectTransform.sizeDelta.y),
        _ => throw new ArgumentOutOfRangeException()
    };
}
