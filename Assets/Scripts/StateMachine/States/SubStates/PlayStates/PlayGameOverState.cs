using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGameOverState : PlayState
{
    [SerializeField] GameObject GameOverScreen = null;
    [SerializeField] Button quitButton = null;

    private void OnEnable()
    {
        quitButton?.onClick.AddListener(Exit);
    }

    private void OnDisable()
    {
        quitButton?.onClick.RemoveListener(Exit);
    }

    private void ActivateElements()
    {
        GameOverScreen.SetActive(true);

    }

    private void DeactivateElements()
    {
        GameOverScreen.SetActive(false);

    }
    public override void Enter()
    {
        ActivateElements();

        base.Enter();
    }

    public override void Exit()
    {
        DeactivateElements();
        base.Exit();
    }

    public override void Pause()
    {
        DeactivateElements();
        base.Pause();
    }

    public override void Resume()
    {
        ActivateElements();
        base.Resume();
    }


}
