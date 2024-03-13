using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINarration : UIPage
{
    [SerializeField] private UIAnimFlipBook _flipBook;
    [SerializeField] private Text _upText;
    [SerializeField] private Text _downText;
    
    private void OnEnable()
    {
        _flipBook.AnimStart();
    }

    private void OnDisable()
    {
        _flipBook.AnimStop();
    }

    public void Init(int contentIndex)
    {
        switch (contentIndex)
        {
            case 0:
                _upText.text = "봄이 오면\n<color=#4580C7>아름다운 꽃</color>도 피고\n<color=#4580C7>동물들</color>의 활동도 활발해져요";
                _downText.text = "<color=#4580C7>퍼즐</color>을 맞추며 살펴볼까요?";
                break;
        }
    }
}
