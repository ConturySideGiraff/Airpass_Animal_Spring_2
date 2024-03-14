using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSG.Puzzle;

public class PuzzleGame : Game
{
    [SerializeField] private BoardHandler _boardHandler;

    private void Awake()
    {
        
    }

    public override float GetDuration()
    {
        return 60.0f * 4.0f;
    }

    public override void Init()
    {
        _boardHandler.Init();
        _boardHandler.gameObject.SetActive(false);
    }

    protected override void Set()
    {
     
    }

    public override void GameStart()
    {
       
    }
}
