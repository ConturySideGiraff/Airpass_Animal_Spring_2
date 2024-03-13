using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UIFade : UIPage
{
    private CanvasGroup _canvasGroup;

    public bool IsFade { get; private set; }

    public async UniTask FadeIn(float duration = 0.5f)
    {
        if (IsFade)
        {
            UniTask.WaitWhile(() => IsFade);
        }

        IsFade = true;

        if (ReferenceEquals(_canvasGroup, null))
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        for (float t = 0.0f; t <= duration; t += Time.deltaTime)
        {
            float percent = t / duration;

            _canvasGroup.alpha = 1.0f - percent;

            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        _canvasGroup.alpha = 0.0f;
        
        IsFade = false;
        
        gameObject.SetActive(false);
    }
    
    public async UniTask FadeOut(float duration = 0.5f)
    {
        if (IsFade)
        {
            UniTask.WaitWhile(() => IsFade);
        }
        
        gameObject.SetActive(true);

        IsFade = true;
        
        if (ReferenceEquals(_canvasGroup, null))
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        for (float t = 0.0f; t <= duration; t += Time.deltaTime)
        {
            float percent = t / duration;

            _canvasGroup.alpha = percent;

            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        _canvasGroup.alpha = 1.0f;
    }
}
