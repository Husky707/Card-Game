using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySM : NestedStateMachine
{
    protected override void OnMachineActivated()
    {
        base.OnMachineActivated();
        ChangeState<DawnState>();
    }

}
