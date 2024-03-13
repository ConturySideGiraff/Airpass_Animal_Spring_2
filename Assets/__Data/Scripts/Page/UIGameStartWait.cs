using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameStartWait : UIPage
{
    [SerializeField] 
    private UIAnimFlipBook _flipBook;
    private Button _button;

    private void Awake()
    {
        _button = _flipBook.gameObject.GetComponent<Button>();
        _button.onClick.AddListener(GameManager.Instance.ToGameInit);
    }

    private void OnEnable()
    {
        _flipBook.AnimStart();
    }

    private void OnDisable()
    {
        _flipBook.AnimStop();
    }
}
