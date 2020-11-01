using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameSM : NestedStateMachine
{


    protected override void OnMachineActivated()
    {
        ChangeState<PlaySetupState>();
    }



}
