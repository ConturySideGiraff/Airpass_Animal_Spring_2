using System;
using System.Collections;
using System.Collections.Generic;
using MonLab;
using UnityEngine;
using UnityEngine.UI;

public class InteractableDelay : MonoBehaviour
{
    [SerializeField] private bool _isAutoStartAtOnEnable;
    [SerializeField] private float _isAutoStartToDuration = 2.0f;
    
    private Button _button;
    private VrButton _vrButton;

    private Action _endAction;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _vrButton = GetComponent<VrButton>();
    }

    public void Init(float duration, Action endAction = null)
    {
        gameObject.SetActive(true);
        
        _endAction = endAction;
        
        
        if(!ReferenceEquals(_coDelay, null)) StopCoroutine(_coDelay);
        _coDelay = CoDelay(duration);
        StartCoroutine(_coDelay);
    }

    private void OnEnable()
    {
        _button.interactable = true;
        _vrButton.Btn.interactable = true;

        if (_isAutoStartAtOnEnable)
        {
            _coDelay = CoDelay(_isAutoStartToDuration);
            StartCoroutine(_coDelay);
        }
    }

    private void OnDisable()
    {
        if (!ReferenceEquals(_coDelay, null)) _coDelay = null;
        _coDelay = null;
    }

    private IEnumerator _coDelay;
    
    private IEnumerator CoDelay(float duration)
    {
        _button.interactable = false;
        _vrButton.Btn.interactable = false;

        for (float t = 0.0f; t <= duration; t += TimerManager.Instance.Reduce)
        {
            yield return null;
        }

        _button.interactable = true;
        _vrButton.Btn.interactable = true;
        
        _endAction?.Invoke();
    }

}
