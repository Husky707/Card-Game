using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayGameSM))]
public class PlayState : State
{

    protected PlayGameSM StateMachine { get; private set; }

    private void Awake()
    {
        StateMachine = GetComponent<PlayGameSM>();
    }
}
