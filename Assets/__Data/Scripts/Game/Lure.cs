using System;
using System.Collections;
using System.Collections.Generic;
using MonLab;
using UnityEngine;

public abstract class Lure : MonoBehaviour
{
    public float lureOnceDuration;

    
    public abstract bool IsCanLure { get; }
    
    public bool IsLure { get; protected set; }
    
    public abstract void Play();

    public abstract void Stop();
}