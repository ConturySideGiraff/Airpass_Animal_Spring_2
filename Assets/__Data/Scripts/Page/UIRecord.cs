using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MonLab;
using UnityEngine;

public class UIRecord : UIPage
{
    [SerializeField] private VrButton _yseButton;
    [SerializeField] private VrButton _noButton;
    [Space]
    [SerializeField] private GameObject _recordTextObject;

    private void Awake()
    {
        _yseButton.Btn.onClick.AddListener(OnYes);
        _noButton.Btn.onClick.AddListener(OnNo);
        
        _recordTextObject.SetActive(false);
    }

    private void OnEnable()
    {
        TimerManager.Instance.QuitTimerStart(GameManager.Instance.ToQuit,60 * 10);
    }

    private void OnDisable()
    {
        TimerManager.Instance.QuitTimerStart(GameManager.Instance.ToQuit,60 * 2);
    }

    private async void OnYes()
    {
        _recordTextObject.SetActive(true);

        foreach (VrButton vrButton in FindObjectsOfType<VrButton>())
        {
            vrButton.interactable = false;
            vrButton.Btn.interactable = false;
        }
        
        await CoYes();
    }

    private IEnumerator CoYes()
    {
        for (float t = 0.0f; t <= 2.0f; t += Time.deltaTime)
        {
            yield return null;
        }
        
        GameManager.Instance.ToQuit();
    }

    private void OnNo()
    {
        GameManager.Instance.ToQuit();
    }
}
