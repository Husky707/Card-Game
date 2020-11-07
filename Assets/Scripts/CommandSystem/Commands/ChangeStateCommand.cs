using System.Collections.Generic;

public class ChangeStateCommand <T, K> : ICommandable where T: State where K: State
{

    private StateMachine controller; 

    public ChangeStateCommand(StateMachine SM,bool executeNow = false)
    {
        controller = SM;

        if (executeNow)
            GameController.GameCommander?.Execute(this);            
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
