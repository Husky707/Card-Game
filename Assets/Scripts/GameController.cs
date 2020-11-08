using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public enum ePlayers {  player, ai }
public class GameController : MonoBehaviour
{
    public event Action GameOverEvent = delegate { };

    public static CommandCenter GameCommander = new CommandCenter();
    public static GameController Game = null;
    public static MasterInput GameInput;

    #region Init
    private void Awake()
    {
        if(Game != null)
        {
            Destroy(this.gameObject);
        }

        Game = this;
        GameInput = gameObject.AddComponent<MasterInput>();
    }

    #endregion

    #region Public Methods

    public void GameOver(ePlayers winner)
    {
        GameOverEvent.Invoke();
    }

    public void QuitGame()
    {
        if (Application.isEditor)
            UnityEditor.EditorApplication.isPlaying = false;
        else
            Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UndoCommand()
    {
        GameCommander.Undo();
    }

    public void RedoCommand()
    {
        GameCommander.Redo();
    }

    #endregion

}
