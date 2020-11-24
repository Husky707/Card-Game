using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using TMPro;

public enum eDayStates { Dawn, Daylight, Dusk }
public class DaytimeController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI dayTimeText = null;
    [SerializeField] GameObject GameOverUI = null;
    [Header("Hooks")]
    [SerializeField] StateMachine DaySM = null;
    [SerializeField] DawnState Dawn = null;
    [SerializeField] DayState Daylight = null;
    [SerializeField] DuskState Dusk = null;

    public eDayStates CurrentState => _currentState;
    private eDayStates _currentState = eDayStates.Dawn;

    #region Init

    bool isInit = false;
    private void Init()
    {
        if (isInit)
            return;

        isInit = true;
        GameController.Game.GameOverEvent += OnGameOver;

    }
    private void OnEnable()
    {
        Dawn.Entered += OnEnteredDawn;
        Daylight.Entered += OnEnteredDaylight;
        Dusk.Entered += OnEnteredDusk;

        GameOverUI.SetActive(false);
    }

    private void OnDisable()
    {
        Dawn.Entered -= OnEnteredDawn;
        Daylight.Entered -= OnEnteredDaylight;
        Dusk.Entered -= OnEnteredDusk;

        GameController.Game.GameOverEvent -= OnGameOver;
    }
    #endregion

    private void OnEnteredDusk()
    {
        dayTimeText.text = "Dusk Phase";
    }

    private void OnEnteredDaylight()
    {
        dayTimeText.text = "Daylight Phase";

    }

    private void OnEnteredDawn()
    {
        dayTimeText.text = "Dawn Phase";

    }

    private void OnGameOver()
    {
        GameOverUI.SetActive(true);
    }


    private void Update()
    {
        Init();
    }

}
