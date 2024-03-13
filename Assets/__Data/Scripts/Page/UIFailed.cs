using System;
using System.Collections;
using System.Collections.Generic;
using MonLab;
using UnityEngine;

public class UIFailed : UIPage
{
    [SerializeField] private VrButton _stopButton;
    [SerializeField] private VrButton _retryButton;

    private void Awake()
    {
        _stopButton.Btn.onClick.AddListener(OnStop);
        _retryButton.Btn.onClick.AddListener(OnRetryButton);
    }

    private void OnEnable()
    {
        TimerManager.Instance.QuitTimerStart(GameManager.Instance.ToQuit, 60 * 10);
    }

    private void OnDisable()
    {
        TimerManager.Instance.QuitTimerStart(GameManager.Instance.ToQuit,60 * 2);
    }
    
    private void OnStop()
    {
        GameManager.Instance.ToQuit();
    }

    private void OnRetryButton()
    {
        GameManager.Instance.ToGameInit();
    }
}
