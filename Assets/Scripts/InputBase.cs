using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eInputStates { Down, Up, Held, Noone }
public enum eComboKeys { Undo }
public abstract class InputBase : MonoBehaviour
{

    public bool IsLocked { get; private set; } = false;

    public void LockInput()
    {
        if (IsLocked)
            return;

        IsLocked = true;
    }

    public void UnlockInput()
    {
        if (!IsLocked)
            return;

        IsLocked = false;
    }

    private void Update()
    {
        if (IsLocked)
            return;

        ReadInput();
    }
    protected abstract void ReadInput();
}
