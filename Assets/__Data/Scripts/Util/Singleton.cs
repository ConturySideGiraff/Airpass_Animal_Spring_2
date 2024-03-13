using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = FindObjectOfType<T>();

            if (_instance != null) return _instance;

            _instance = new GameObject($"Instance_", typeof(T)).GetComponent<T>();

            return _instance;
        }
    }
}