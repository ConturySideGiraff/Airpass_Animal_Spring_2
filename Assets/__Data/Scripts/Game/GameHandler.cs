using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private List<Game> _games;

    public int CurrentContentIndex { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.Instance.ToGameInit();
        }
    }

    public void OffAll()
    {
        foreach (Game game in _games)
        {
            game.Off();
        }
    }

    public void On(int index)
    {
        CurrentContentIndex = index;
        
        _games[index].On();
    }

    public void Init(int index)
    {
        CurrentContentIndex = index;

        _games[index].Init();
    }

    public float GetDuration(int index)
    {
        CurrentContentIndex = index;

        return _games[index].GetDuration();
    }

    public void GameStart(int index)
    {
        CurrentContentIndex = index;

        _games[index].GameStart();
    }

    public void Interactable(int index, bool isInteractable)
    {
        CurrentContentIndex = index;

        // _games[index].Interactable(isInteractable);
    }

    public void InteractableDelay(int index, bool isInteractable, float delay)
    {
        CurrentContentIndex = index;

        // _games[index].InteractableDelay(isInteractable, delay);
    }
}
