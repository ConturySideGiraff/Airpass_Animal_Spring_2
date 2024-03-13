using System;
using System.Collections;
using System.Collections.Generic;
using MonLab;
using Unity.VisualScripting;
using UnityEngine;

public class UIHome : UIPage
{
    [SerializeField] private VrButton _yesButton;
    [SerializeField] private VrButton _noButton;

    private void Awake()
    {
        _yesButton.Btn.onClick.AddListener(OnYes);
        _noButton.Btn.onClick.AddListener(OnNo);
    }

    private void OnEnable()
    {
        TimerManager.Instance.QuitTimerStart(GameManager.Instance.ToQuit, 60 *10);
    }

    private void OnDisable()
    {
        if (UIManager.Instance.Get<UISetting>().gameObject.activeInHierarchy)
        {
            TimerManager.Instance.QuitTimerStart(GameManager.Instance.ToQuit, 60 *2);
        }
        
        GameManager.Instance.OffPopup();
    }

    private void OnYes()
    {
        GameManager.Instance.ToQuit();
    }

    private void OnNo()
    {
        if (!UIManager.Instance.Get<UISetting>().gameObject.activeInHierarchy)
        {
            AudioManager.Instance.NarrationUnPause();
        }

        Off();
    }
}
