using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ePauseMenues {  Main, Escape, Noone }
public class PauseMenu : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject InGamePauseMenu = null;
    [SerializeField] GameObject EscapeMenu = null;

    [Header("Requirments")]
    [SerializeField] CardGameSM CoreSM = null;
    [SerializeField] PlayGameSM PlaySM = null;

    private bool isInit = false;
    private ePauseMenues openMenu = ePauseMenues.Noone;


    #region Init
    private void Awake()
    {
        InGamePauseMenu?.SetActive(false);
        EscapeMenu?.SetActive(false);
    }
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (isInit)
            return;
        if (GameController.GameInput == null)
        {
            Debug.Log("Could not init Pause menu. No GameController");
            return;
        }

        isInit = true;
        GameController.GameInput.Escape += OnEscape;
    }


    #endregion



    private void OnEscape(eInputStates  state)
    {
        bool isOpen = true;
        if (openMenu == ePauseMenues.Noone)
        {
            openMenu = DetermineMenutype();
            isOpen = false;
        }

        if (openMenu == ePauseMenues.Escape)
        {
            if (isOpen)
                CloseMenu();
            else
            {
                EscapeMenu.SetActive(true);
            }            
        }
        else if(openMenu == ePauseMenues.Main)
        {
            if (isOpen)
                CloseMenu();
            else
            {
                InGamePauseMenu.SetActive(true);
                PlaySM.PauseMachine();
            }
        }
        
    }

    public void CloseMenu()
    {
        if (openMenu == ePauseMenues.Noone)
            return;

        openMenu = ePauseMenues.Noone;
        EscapeMenu.SetActive(false);
        InGamePauseMenu.SetActive(false);
        PlaySM.ResumeMachine();
    }


    private ePauseMenues DetermineMenutype()
    {
        if(CoreSM.CurrentState?.GetType() == typeof(MainMenueState) || PlaySM.CurrentState?.GetType() == typeof(PlayGameOverState))
        {
            return ePauseMenues.Escape;
        }

        return ePauseMenues.Main;
    }
}
