using System.Collections;
using System.Collections.Generic;

public class CommandCenter
{
    private Stack<ICommandable> CommandHistory = new Stack<ICommandable>();
    private Stack<ICommandable> UndoHistory = new Stack<ICommandable>(0);

    public void Execute(ICommandable command)
    {
        CommandHistory.Push(command);
        command.Execute();

        ClearUndoHistory();
    }

    public void Undo()
    {
        if (CommandHistory.Count <= 0)
        {
            UnityEngine.Debug.Log("Nothing to Undo");
            return;
        }

        ICommandable undoneCommand = CommandHistory.Pop();
        UndoHistory.Push(undoneCommand);

        undoneCommand.Undo();
    }

    public void Redo()
    {
        if(UndoHistory.Count <= 0)
        {
            UnityEngine.Debug.Log("Nothing to Redo");
            return;
        }

        ICommandable redoneCommand = UndoHistory.Pop();
        CommandHistory.Push(redoneCommand);
        redoneCommand.Execute();        
    }

    private void ClearUndoHistory()
    {
        if (UndoHistory.Count <= 0)
            return;

        UndoHistory = new Stack<ICommandable>(0);
    }
}
