using System;
using System.Collections;
using System.Collections.Generic;
using MonLab;
using UnityEngine;

public class UIDisplay : UIPage
{
    [SerializeField] private VrButton _homeButton;
    [SerializeField] private VrButton _settingButton;

    private UIHome _uiHome;
    private UISetting _uiSetting;
    
    private void Awake()
    {
        _homeButton.Btn.onClick.AddListener(OnHome);
        _settingButton.Btn.onClick.AddListener(OnSetting);

        _uiHome = UIManager.Instance.Get<UIHome>();
        _uiSetting = UIManager.Instance.Get<UISetting>();
    }

    private bool IsTimePause => _uiHome.gameObject.activeInHierarchy || _uiSetting.gameObject.activeInHierarchy;

    private void OnHome()
    {
        UIHome uiHome = UIManager.Instance.Get<UIHome>();

        if (uiHome.gameObject.activeInHierarchy)
        {
            GameManager.Instance.OffPopup();
            uiHome.Off();
        }
        
        else
        {
            uiHome.On();
            GameManager.Instance.OnPopup(IsTimePause);
        }
    }

    private void OnSetting()
    {
        UISetting uiSetting = UIManager.Instance.Get<UISetting>();

        if (uiSetting.gameObject.activeInHierarchy)
        {
            GameManager.Instance.OffPopup();
            uiSetting.CloseAsync();
        }

        else
        {
            uiSetting.On();
            uiSetting.OpenAsync();
            GameManager.Instance.OnPopup(IsTimePause);
        }
    }
}
