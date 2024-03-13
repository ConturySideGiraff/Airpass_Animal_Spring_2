using System;
using System.Collections;
using System.Collections.Generic;
using MonLab;
using UnityEngine;
using UnityEngine.Serialization;

public class TimerManager : Singleton<TimerManager>
{
    // ___ Game ___
    [Header("[ Game ]")]
    [SerializeField] private List<GameObject> _stopObjectList = new List<GameObject>();
    
    public float Reduce
    {
        get
        {
            foreach (GameObject o in _stopObjectList)
            {
                if (o.activeInHierarchy)
                {
                    return 0;
                }
            }

            return Time.deltaTime;
        }
    }
    
    [SerializeField] private float _gameDuration;
    [SerializeField] private float _gameTimer;

    public float GameTimer => _gameTimer;
    public Action OnGameInit { get; set; }

    public void GameTimerInit(float duration)
    {
        _gameDuration = duration;
        _gameTimer = duration;
        OnGameInit?.Invoke();
    }

    public void GameTimerStart()
    {
        _coGameTimer = CoGameTimer();
        StartCoroutine(_coGameTimer);
    }

    public void GameTimerStop()
    {
        if(!ReferenceEquals(_coGameTimer, null)) StopCoroutine(_coGameTimer);
        _coGameTimer = null;
    }

    public float GameTimerPercent()
    {
        return GameTimer / _gameDuration;
    }
    

    private IEnumerator _coGameTimer;

    private IEnumerator CoGameTimer()
    {
        while (_gameTimer >= 0.0f)
        {
            _gameTimer -= Reduce;
            yield return null;
        }
        
        GameManager.Instance.ToResult(false);
    }
    
    // ___ Lure ___
    
    [Header("[ Lure ]")] 
    [SerializeField] private float _lureAwaitDuration = 5.0f;
    [SerializeField] private float _lureTimer;

    public Action OnLure { get; set; }
    
    public void LureTimerStart()
    {
        LureTimerStop();
        
        _coLureTimer = CoLureTimer();
        StartCoroutine(_coLureTimer);
    }

    public void LureTimerStop()
    {
        _lureTimer = _lureAwaitDuration;

        if(!ReferenceEquals(_coLureTimer, null)) StopCoroutine(_coLureTimer);
        _coLureTimer = null;
    }

    private IEnumerator _coLureTimer;

    private IEnumerator CoLureTimer()
    {
        while (_lureTimer >= 0.0f)
        {
            _lureTimer -= Reduce;
            yield return null;
        }
        
        OnLure.Invoke();
    }
    
    // ___ Quit ___
    [Space]
    [SerializeField] private List<GameObject> _popupList = new List<GameObject>();
    [SerializeField] private List<VrButton> _quitResetButtonList = new List<VrButton>();
    [Space]
    [SerializeField] private float _quitAwaitDuration;
    [SerializeField] private float _quitTimer;

    public Action _onQuitAction;


    private void Awake()
    {
        foreach (VrButton vrButton in _quitResetButtonList)
        {
            vrButton.Btn.onClick.RemoveListener(QuitTimerReset);
            vrButton.Btn.onClick.AddListener(QuitTimerReset);
        }
    }

    public void QuitTimerStart(Action action, float duration = 120.0f)
    {
        _onQuitAction = action;
        _quitAwaitDuration = duration;
        
        QuitTimerStop();
        
        _coQuitTimer = CoQuiteTimer();
        StartCoroutine(_coQuitTimer);
    }

    public void QuitTimerReset()
    {
        _quitTimer = _quitAwaitDuration;
    }

    private void QuitTimerStop()
    {
        _quitTimer = _quitAwaitDuration;
        
        if(!ReferenceEquals(_coQuitTimer, null)) StopCoroutine(_coQuitTimer);
        _coQuitTimer = null;
    }

    private float QuiteReduce {
        get
        {
            foreach (GameObject o in _popupList)
            {
                if (o.gameObject.activeInHierarchy) return Time.deltaTime;
            }

            return 0;
        }
    }

    private IEnumerator _coQuitTimer;

    private IEnumerator CoQuiteTimer()
    {
        while (_quitTimer >= 0.0f)
        {
            _quitTimer -= QuiteReduce;
            yield return null;
        }
        
        GameManager.Instance.ToQuit();
    }
}
