using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITitle : UIPage
{
    public void AnimStartAll()
    {
        UIAnim[] animInterfaces = FindObjectsOfType<UIAnim>();

        foreach (UIAnim uiAnim in animInterfaces)
        {
            uiAnim.AnimStart();
        }
    }
}
