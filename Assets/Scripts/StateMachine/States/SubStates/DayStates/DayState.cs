using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DaySM))]
public class DayState : State
{

    protected DaySM StateMachine { get; private set; }

    private void Awake()
    {
        StateMachine = GetComponent<DaySM>();
    }
}
