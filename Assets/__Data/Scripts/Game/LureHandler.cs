using System;
using System.Collections;
using System.Collections.Generic;
using CSG.Puzzle;
using MonLab;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class LureHandler : MonoBehaviour
{
    [SerializeField] protected float _timer;
    [SerializeField] protected float _timeMax = 5.0f;
    [SerializeField] protected float _lureOnceDuration = 1.0f;
    [SerializeField] protected List<Lure> _lureList = new List<Lure>();
    
    protected bool _isLureWait;

    protected abstract void LureListInit();

    protected abstract void ValueInit();

    protected abstract void OnLure();


    protected void Awake()
    {
        LureListInit();
    }

    protected virtual void OnEnable()
    {
        LureWaitStart();
    }

    protected virtual void OnDisable()
    {
        LureWaitStop();
    }

    public virtual void LureWaitStart()
    {
        LureWaitStop();

        _isLureWait = true;
        _timer = 0.0f;

        _coLureWait = CoLureWait();
        StartCoroutine(_coLureWait);
    }
    
    public virtual void LureWaitStop()
    {
        ValueInit();
        
        _isLureWait = false;
        _timer = 0.0f;

        foreach (Lure lure in _lureList)
        {
            lure.Stop();
        }
        
        if(!ReferenceEquals(_coLureWait, null)) StopCoroutine(_coLureWait);
        _coLureWait = null;
    }

    private IEnumerator _coLureWait = null;

    private IEnumerator CoLureWait()
    {
        while (gameObject.activeInHierarchy)
        {
            _timer += TimerManager.Instance.Reduce * (_isLureWait ? 1.0f : 0.0f);

            if (_timer > _timeMax && _isLureWait)
            {
                OnLure();   
            }

            yield return null;
        }
    }

 

    public void OnLureReset()
    {
        _timer = 0.0f;
        _isLureWait = true;
        
        foreach (Lure lure in _lureList)
        {
            if (lure.IsLure)
            {
                lure.Stop();
            }
        }
    }
}