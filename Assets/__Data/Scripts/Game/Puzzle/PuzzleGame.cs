using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSG.Puzzle;

public class PuzzleGame : Game
{
    [SerializeField] private BoardSelectHandler _boardSelectHandler;
    [SerializeField] private ClearEffectHandler _clearEffectHandler;
    [SerializeField] private BoardHandler _boardHandler;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Init();
        }
    }

    public override float GetDuration()
    {
        return 60.0f * 4.0f;
    }

    public override void Init()
    {
        _boardHandler.Init();
        _boardHandler.gameObject.SetActive(false);
        
        _boardSelectHandler.gameObject.SetActive(false);
        _boardSelectHandler.gameObject.SetActive(true);
        
        _clearEffectHandler.gameObject.SetActive(false);
        _clearEffectHandler.gameObject.SetActive(true);
    }

    protected override void Set()
    {
        
    }

    public override void GameStart()
    {
    }
}
