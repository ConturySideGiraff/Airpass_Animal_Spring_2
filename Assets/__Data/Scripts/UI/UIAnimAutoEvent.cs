using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimAutoEvent : MonoBehaviour
{
    private UIAnim _uiAnim;

    private void Awake()
    {
        _uiAnim = GetComponent<UIAnim>();
    }

    private void OnEnable()
    {
        _uiAnim.AnimStart();
    }

    private void OnDisable()
    {
        _uiAnim.AnimStop();
    }
}
