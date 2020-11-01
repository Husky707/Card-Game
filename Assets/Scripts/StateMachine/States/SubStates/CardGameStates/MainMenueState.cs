using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenueState : CardGameState
{
    [SerializeField] GameObject MenuCanvas = null;

    public override void Enter()
    {
        MenuCanvas?.SetActive(true);

        base.Enter();
    }

    public void PlayGame()
    {
        StateMachine.ChangeState<PlayGameState>();
    }

    public void QuitGame()
    {
        if (Application.isEditor)
            UnityEditor.EditorApplication.isPlaying = false;
        else
            Application.Quit();
    }

    public override void Exit()
    {
        MenuCanvas?.SetActive(false);

        base.Exit();
    }
}
