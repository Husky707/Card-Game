using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySetupState : PlayState
{
    [SerializeField] GameObject GameCanvas = null;
    public override void Enter()
    {
        SetUpGame();

        //After Setup
        base.Enter();
        StateMachine.ChangeState<PlayDayState>();
    }

    private void SetUpGame()
    {
        GameCanvas?.SetActive(true);
        CreateDecks();
    }

    private void CreateDecks()
    {
        throw new NotImplementedException();
    }
}
