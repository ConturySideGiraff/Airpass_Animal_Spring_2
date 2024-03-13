using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSG.Puzzle;

public class PuzzleGame : Game
{
    public enum PuzzleSet
    {
        CherryBlossom ,
        Forsythia,
        Frog = 2,
        Butterfly = 3,
        Chick = 4,
        Magnolia,
        Azalea,
    }
    
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
    }

    protected override void Set()
    {
     
    }

    public override void GameStart()
    {
       
    }
}
