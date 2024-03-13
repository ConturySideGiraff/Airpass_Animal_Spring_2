using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIAnim : MonoBehaviour, IUIAnimInterface
{
    protected RectTransform _rectTransform;
    protected CanvasGroup _canvasGroup;

    protected Vector2 _startVector2;

    protected Action _endAction;
    
    protected virtual void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _startVector2 = _rectTransform.anchoredPosition;
    }

    public void AnimStart(Action endAction = null)
    {
        _endAction = endAction;
        
        _coAnim = CoAnim();
        StartCoroutine(_coAnim);
    }

    public virtual void AnimStop()
    {
        if(!ReferenceEquals(_coAnim, null)) StopCoroutine(_coAnim);
        _coAnim = null;
    }

    private IEnumerator _coAnim;

    protected abstract IEnumerator CoAnim();
}
