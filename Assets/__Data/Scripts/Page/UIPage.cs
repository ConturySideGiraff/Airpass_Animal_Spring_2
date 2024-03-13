using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPage : MonoBehaviour
{
    public UIPage On()
    {
        gameObject.SetActive(true);

        return this;
    }

    public UIPage Off()
    {
        gameObject.SetActive(false);

        return this;
    }
}
