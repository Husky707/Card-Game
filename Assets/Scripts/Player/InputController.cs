using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public enum eInputStates { Down, Up, Held, Noone}
public enum eComboKeys { Undo }
public class InputController : MonoBehaviour
{
    public event Action<eInputStates> LeftMouse = delegate { };
    public event Action<eInputStates> RightMouse = delegate { };
    public event Action<eInputStates> Undo = delegate { };
    public event Action<eInputStates> Esc = delegate { };
    public event Action<eInputStates> Enter = delegate { };

    [SerializeField] bool isReadingInput = true;

    Dictionary<eComboKeys, Dictionary<KeyCode, eInputStates> > keyCombos = new Dictionary<eComboKeys, Dictionary<KeyCode, eInputStates> >();

    bool isInEditor = false;

    #region Init
    private void Start()
    {
        if (Application.isEditor)
            isInEditor = true;

        InitComboKeys();    
    }
    void InitComboKeys()
    {

        Dictionary<KeyCode, eInputStates> undoStates = new Dictionary<KeyCode, eInputStates>();
        if (isInEditor)
        {
            undoStates.Add(KeyCode.Z, eInputStates.Noone);
        }
        else
        {
            undoStates.Add(KeyCode.Z, eInputStates.Noone);
            undoStates.Add(KeyCode.LeftControl, eInputStates.Noone);
        }

        keyCombos.Add(eComboKeys.Undo, undoStates);
    }

    void InvokeComboEvent(eComboKeys combo, eInputStates state)
    {
        switch (combo)
        {
            case eComboKeys.Undo:
                Undo.Invoke(state);
                break;
        }
    }

    #endregion

    void Update()
    {
        if (!isReadingInput)
            return;

        GetMouseInput();
        GetKeyInput();
        GetComboInput();
    }

    public void LockInput()
    {
        SetInputLock(false);
    }

    public void UnlockInput()
    {
        SetInputLock(true);
    }


    #region Read Input

    void GetKeyInput()
    {

        if (Input.GetKeyDown(KeyCode.Return))
            Enter.Invoke(eInputStates.Down);
        else if (Input.GetKeyUp(KeyCode.Return))
            Enter.Invoke(eInputStates.Up);

        if (Input.GetKeyDown(KeyCode.Escape))
            Esc.Invoke(eInputStates.Down);
        else if (Input.GetKeyUp(KeyCode.Escape))
            Esc.Invoke(eInputStates.Up);
    }

    void GetMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouse.Invoke(eInputStates.Down);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            LeftMouse.Invoke(eInputStates.Up);
        }

        if (Input.GetMouseButtonDown(1))
        {
            RightMouse.Invoke(eInputStates.Down);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            RightMouse.Invoke(eInputStates.Up);

        }

    }

    void GetComboInput()
    {
        foreach (eComboKeys inputCombo in keyCombos.Keys)
        {
            //Create an array of each key in the combo
            int numKeys = keyCombos[inputCombo].Keys.Count;
            KeyCode[] keys = new KeyCode[numKeys];
            int i = 0;
            foreach (KeyCode key in keyCombos[inputCombo].Keys)
            { 
                keys[i] = key;
                i++;
            }

            //Update each key's state
            foreach(KeyCode key in keys)
            { 

                if (Input.GetKeyDown(key))
                    keyCombos[inputCombo][key] = eInputStates.Down;
                else if (Input.GetKeyUp(key))
                    keyCombos[inputCombo][key] = eInputStates.Up;
                else if (Input.GetKey(key))
                    keyCombos[inputCombo][key] = eInputStates.Held;
                else
                    keyCombos[inputCombo][key] = eInputStates.Noone;
            }

            //Create a list of the states of each key in the combo
            List<eInputStates> stateList = new List<eInputStates>(keyCombos[inputCombo].Count);
            foreach (eInputStates state in keyCombos[inputCombo].Values)
                stateList.Add(state);

            eInputStates newState = GetComboState(stateList);
            //Debug.Log(inputCombo.ToString() + " state: " + newState.ToString());
            if (newState != eInputStates.Noone && newState != eInputStates.Held)
                InvokeComboEvent(inputCombo, newState);
        }
    }

    #endregion

    #region Helpers

    void SetInputLock(bool toState)
    {
        isReadingInput = toState;
    }

    eInputStates GetComboState(List<eInputStates> currentStates)
    {
        bool anyDown = false;
        bool anyUp = false;

        foreach (eInputStates state in currentStates)
        {
            if (state == eInputStates.Noone)
                return eInputStates.Noone;

            if (state == eInputStates.Held)
            {
                continue;
            }

            if (state == eInputStates.Up)
            {
                anyUp = true;
                continue;
            }

            if (state == eInputStates.Down)
            {
                anyDown = true;
            }
        }

        if (anyUp)
            return eInputStates.Up;

        if (anyDown)
            return eInputStates.Down;
        else return eInputStates.Held;

    }
    #endregion
}
