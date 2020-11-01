using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestedStateMachine : StateMachine
{
    public bool isRunning { get; protected set; } = false;
    private bool hasActivated = false;

    public virtual void ActivateMachine()
    {
        if (hasActivated)
            return;

        isRunning = true;
        hasActivated = true;
        OnMachineActivated();
    }

    public void ResumeMachine()
    {
        if (isRunning || !hasActivated)
            return;

        isRunning = true;
        CurrentState?.Resume();
    }

    public void PauseMachine()
    {
        if (!isRunning || !hasActivated)
            return;

        isRunning = false;
        CurrentState?.Pause();
    }

    protected override void Update()
    {
        if (!isRunning || !hasActivated)
            return;

        base.Update();
    }

    protected virtual void OnMachineActivated(){ }
}
