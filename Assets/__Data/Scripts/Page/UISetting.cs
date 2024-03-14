using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MonLab;
using UnityEngine;

public class UISetting : UIPage
{
    [Space]
    [SerializeField] private RectTransform _conentRt;
    [SerializeField] private float _duration = 1.0f;
    [Space]
    [SerializeField] private VrButton[] _sfxButtons = new VrButton[3];
    [SerializeField] private VrButton[] _bgButtons = new VrButton[3];
    [Space]
    [SerializeField] private Sprite[] _offSprite = new Sprite[3];
    [SerializeField] private Sprite[] _onSprites = new Sprite[3];

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            int index = i;
            _sfxButtons[index].Btn.onClick.AddListener(() => OnSfx(index));
            _bgButtons[index].Btn.onClick.AddListener(() => OnBgm(index));
        }
    }

    private void OnEnable()
    {
        AudioManager.Instance.SfxPlay(AudioClipName.Popup);
        
        TimerManager.Instance.QuitTimerStart(delegate
        {
            UIManager.Instance.On<UIHome>();
        });
    }

    private void OnDisable()
    {
        TimerManager.Instance.QuitTimerStart(GameManager.Instance.ToQuit, 60 * 2);

        GameManager.Instance.OffPopup();
    }

    private void OnSfx(int index)
    {
        for (var i = 0; i < _sfxButtons.Length; i++)
        {
            _sfxButtons[i].Btn.image.sprite = _offSprite[i];
        }

        _sfxButtons[index].Btn.image.sprite = _onSprites[index];
        
        AudioManager.Instance.SetVolumeSfx(index);
    }

    private void OnBgm(int index)
    {
        for (var i = 0; i < _bgButtons.Length; i++)
        {
            _bgButtons[i].Btn.image.sprite = _offSprite[i];
        }

        _bgButtons[index].Btn.image.sprite = _onSprites[index];
        
        AudioManager.Instance.SetVolumeBgm(index);
    }
    
    //

    private bool IsSlide { get; set; }

    public async void OpenAsync()
    {
        if (IsSlide)
        {
            return;
        }

        IsSlide = true;

        Vector2 startPoint = new Vector2(617, 0);
        Vector2 endPoint = new Vector2(-11, 0);
        
        await Slide(startPoint, endPoint);

        _conentRt.anchoredPosition = endPoint;
        
        IsSlide = false;
    }

    public async void CloseAsync()
    {
        if (IsSlide)
        {
            return;
        }

        IsSlide = true;
        
        Vector2 startPoint = new Vector2(-11, 0);
        Vector2 endPoint = new Vector2(617, 0);

        await Slide(startPoint, endPoint);
        
        _conentRt.anchoredPosition = endPoint;
        
        IsSlide = false;

        AudioManager.Instance.NarrationUnPause();
        
        Off();
    }

    private IEnumerator Slide(Vector2 startPoint, Vector2 endPoint)
    {
        for (float t = 0.0f; t <= _duration; t += Time.deltaTime)
        {
            float percent = t / _duration;

            _conentRt.anchoredPosition = Vector2.Lerp(startPoint, endPoint, percent);
            
            yield return null;
        }

        _conentRt.anchoredPosition = endPoint;
    }
}
