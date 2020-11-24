using System.Collections.Generic;
using System.Diagnostics;

public class ChangeStateCommand <T, K> : ICommandable where T: State where K: State
{

    private StateMachine controller; 

    public ChangeStateCommand(StateMachine SM)
    {
        controller = SM;
        if (SM == null)
            UnityEngine.Debug.Log("Failed to give a valid State machine to change state command");
    }

    public void Execute()
    {
        controller.ChangeState<T>();
    }

    public void Undo()
    {
        controller.ChangeState<K>();
    }
}
