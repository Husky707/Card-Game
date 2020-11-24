using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DaySM))]
public class DayState : State
{

    protected DaySM StateMachine { get; private set; }

    protected virtual void Awake()
    {
        StateMachine = GetComponent<DaySM>();
        if (StateMachine == null)
            Debug.Log("Could not find a valid state machine for DayState");
    }
}
