using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameState : CardGameState
{

    [SerializeField] PlayGameSM NestedStateMachine = null;

    public override void Enter()
    {
        NestedStateMachine?.ActivateMachine();
        NestedStateMachine?.ResumeMachine();

        base.Enter();
    }

    public override void Exit()
    {
        NestedStateMachine?.PauseMachine();

        base.Exit();
    }
}
