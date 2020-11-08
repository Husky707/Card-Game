using UnityEngine;
using UnityEngine.UI;

public class DawnState : DayState
{

    [SerializeField] GameObject DawnUI = null;
    [SerializeField] Button endDawnButton = null;


    #region Init
    protected override void Awake()
    {
        base.Awake();
        DawnUI.SetActive(false);
    }
    private void OnEnable()
    {
        endDawnButton?.onClick.AddListener(EndDawnState);

    }

    private void OnDisable()
    {
        endDawnButton?.onClick.RemoveListener(EndDawnState);
    }

    #endregion


    public void EndDawnState()
    {
        ChangeStateCommand<DaylightState, DawnState> stateChange = new ChangeStateCommand<DaylightState, DawnState>(StateMachine);
        stateChange.Execute();
        Exit();
    }

    private void OnEndDawn()
    {
        DawnUI.SetActive(false);
    }
    
    private void OnBeginDawn()
    {
        DawnUI.SetActive(true);
    }

    public override void Exit()
    {
        OnEndDawn();

        base.Exit();
    }

    public override void Enter()
    {
        OnBeginDawn();

        base.Enter();
    }

}
