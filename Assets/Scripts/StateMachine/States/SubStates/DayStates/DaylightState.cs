using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum eTurn { player, ai} 
public class DaylightState : DayState
{
    public event Action AITurnEnded = delegate { };
    public event Action AITurnStarted = delegate { };
    public event Action PlayerTurnStarted = delegate { };
    public event Action PlayerTurnEnded = delegate { };

    [SerializeField] int ActionsPerDaylight = 3;
    [SerializeField] BankController Bank = null;
    public eTurn CurrentTurn => _currentTurn;
    private eTurn _currentTurn = eTurn.player;

    public int ActionCount => _actionCount;
    private int _actionCount = 0;


    public void LeaveDaylightState()
    {
        ChangeStateCommand<DawnState, DaylightState> stateChange = new ChangeStateCommand<DawnState, DaylightState>(StateMachine);
        stateChange.Execute();
        Exit();
    }


    private void OnExit()
    {
        _actionCount = 0;
    }

    private void OnEnter()
    {
        BeginPlayerTurn();
    }

    #region Turns
    private void BeginPlayerTurn()
    {
        _currentTurn = eTurn.player;
        PlayerTurnStarted.Invoke();
    }

    public void EndPlayerTurn()
    {
        PlayerTurnEnded.Invoke();
        _actionCount++;
        BeginAITurn();
    }

    private void BeginAITurn()
    {
        AITurnStarted.Invoke();
        _currentTurn = eTurn.ai;
    }

    private void AIResolveTurn()
    {
        Bank.AIGainGold(UnityEngine.Random.Range(1, 4));
        EndAITurn();
    }

    private void EndAITurn()
    {
        AITurnEnded.Invoke();

        if (ActionCount >= ActionsPerDaylight)
        {
            LeaveDaylightState();
        }
        else
        {
            BeginPlayerTurn();
        }

    }

    #endregion

    private float enemyTurnDuration = 2f;
    private float aiTick = 0f;
    public override void Tick()
    {
        base.Tick();
        if(CurrentTurn == eTurn.ai)
        {
            aiTick += Time.deltaTime;
            if(aiTick >= enemyTurnDuration)
            {
                aiTick = 0f;
                enemyTurnDuration = UnityEngine.Random.Range(0.75f, 3.5f);
                AIResolveTurn();
            }
        }
    }

    public override void Exit()
    {
        OnExit();
        base.Exit();
    }

    public override void Enter()
    {
        OnEnter();
        base.Enter();
    }

    private void PlayerGainedGold(ePlayers target, int total)
    {
        if (total >= Bank.GoldToWin)
        {
            Debug.Log(target.ToString() + " wins!");
            GameController.Game.GameOver(target);
        }
    }

    private void OnEnable()
    {
        Bank.GoldTotalChanged += PlayerGainedGold;
    }

    private void OnDisable()
    {
        Bank.GoldTotalChanged -= PlayerGainedGold;

    }
}
