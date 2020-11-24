using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDayState : PlayState
{
    [SerializeField] NestedStateMachine NestedSM = null;

    public override void Enter()
    {

        base.Enter();

        NestedSM.ActivateMachine();
        NestedSM.ResumeMachine();
    }

    public override void Exit()
    {
        base.Exit();
        NestedSM.PauseMachine();

    }
}
