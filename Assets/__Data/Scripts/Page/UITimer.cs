using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : UIPage
{
    [SerializeField] private Image _gageImage;
    [SerializeField] private UIAnimScaleLoop _clockAnim;

    private TimerManager _timerManager;

    private bool IsAnim { get; set; }
    
    private void Init()
    {
        _clockAnim.AnimStop();
        _clockAnim.gameObject.transform.localScale = Vector3.one;

        IsAnim = false;
    }

    private void Awake()
    {
        _timerManager = TimerManager.Instance;
        _timerManager.OnGameInit += Init;
    }

    private void Update()
    {
        float percent = _timerManager.GameTimerPercent();

        if (!IsAnim && _timerManager.GameTimer <= 5f)
        {
            _clockAnim.AnimStart();
            IsAnim = true;
        }

        _gageImage.fillAmount = percent;
    }

    private void LateUpdate()
    {
    }
}
