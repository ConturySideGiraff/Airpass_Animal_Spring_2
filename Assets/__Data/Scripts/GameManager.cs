using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public partial class GameManager : Singleton<GameManager>
{
    [SerializeField] private int _contentIndex = 0;
    
    private UIManager _uiManager;
    private GameHandler _gameHandler;
    private TimerManager _timerManager;

    private void Awake()
    {
        PlatformProtocol.SendData(Cmd.Load);
    }

    public void Start()
    {
        _uiManager = UIManager.Instance;
        _timerManager = TimerManager.Instance;
        _gameHandler = GetComponent<GameHandler>();
        
        ToTitle();
    }
}

public partial class GameManager
{
    private async UniTask Delay(float duration)
    {
        for (float t = 0.0f; t <= duration; t += Time.deltaTime)
        {
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
    }

    private UIFade UIFade => UIManager.Instance.Get<UIFade>();

    public void ContentClear()
    {
        ++_contentIndex;
    }
}

public partial class GameManager
{

    private async void ToTitle()
    {
        PlatformProtocol.SendData(Cmd.Home);

        _uiManager.OffAll();
        _uiManager.On<UITitle>().AnimStartAll();

        await Delay(5.5f);
        
        ToGameWait();
    }

    private void ToGameWait()
    {
        PlatformProtocol.SendData(Cmd.Ready);
        
        // quit
        TimerManager.Instance.QuitTimerStart(GameManager.Instance.ToQuit);
        
        // ui
        _uiManager.On<UIGameStartWait>();
    }

    public async void ToGameInit()
    {
        await UIFade.FadeOut();
        
        _gameHandler.OffAll();
        _gameHandler.Init(_contentIndex);
        float gameDuration = _gameHandler.GetDuration(_contentIndex);
        
        _timerManager.GameTimerInit(gameDuration);
        
        ToNarration();
    }

    private async void ToNarration()
    {
        PlatformProtocol.SendData(Cmd.Start);
        
        _uiManager.OffAll();
        _uiManager.On<UIDisplay>();
        _uiManager.On<UITimer>();
        
        _gameHandler.On(_contentIndex);

        UINarration narration = UIManager.Instance.On<UINarration>();
        narration.Init(_contentIndex);
        
        await UIFade.FadeIn();

        float narrationDuration = AudioManager.Instance.NarrationPlay(_contentIndex, out AudioSource narrationAudioSource);
        float percent = 0.0f;
        
        while (percent < 0.98f)
        {
            percent = narrationAudioSource.time / narrationDuration;
            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        await Delay(0.5f);

        ToGame();
    }

    private void ToGame()
    {
        _ = UIManager.Instance.Off<UINarration>();

        _timerManager.GameTimerStart();
        _gameHandler.GameStart(_contentIndex);
    }

    public void ToResult(bool isClear)
    {
        PlatformProtocol.SendData(Cmd.End);

        if (isClear)
        {
            UIManager.Instance.On<UIRecord>();
        }

        else
        {
            UIManager.Instance.On<UIFailed>();
        }
    }

    public void OnPopup(bool isTimePause)
    {
        if (isTimePause)
        {
            AudioManager.Instance.NarrationPause();
        }
        
        else
        {
            AudioManager.Instance.NarrationUnPause();
        }
    }

    public void OffPopup()
    {
        _gameHandler.Interactable(_contentIndex, false);
        _gameHandler.InteractableDelay(_contentIndex, true, 2.0f);
        
        TimerManager.Instance.QuitTimerReset();
    }

    public void ToQuit()
    {
        PlatformProtocol.SendData(Cmd.Close);
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }
}

