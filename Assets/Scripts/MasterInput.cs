using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MasterInput : InputBase
{
    public event Action<eInputStates> Escape = delegate { };

    protected override void ReadInput()
    {
        ReadKeys();
    }

    private void ReadKeys()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Escape.Invoke(eInputStates.Down);
        }
    }
}
