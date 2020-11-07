using UnityEngine;
using UnityEngine.UI;

public class DawnState : DayState
{
    [SerializeField] GameObject DawnUI = null;
    [SerializeField] Button endDawnButton = null;

    #region Init
    private void Awake()
    {
        DawnUI.SetActive(false);
    }
    private void OnEnable()
    {
        endDawnButton?.onClick.AddListener(EndDawn);
    }

    private void OnDisable()
    {
        endDawnButton?.onClick.RemoveListener(EndDawn);
    }

    #endregion

    /// <summary>
    /// /Needs to be the other way around!!1
    /// </summary>
    public void EndDawn()
    {
        DawnUI.SetActive(false);
        Exit();
    }
    
    public void BeginDawn()
    {
        DawnUI.SetActive(true);
        Enter();
    }

    public override void Exit()
    {
        ChangeStateCommand<DaylightState, DawnState> stateChange = new ChangeStateCommand<DaylightState, DawnState>(StateMachine, true);
        base.Exit();
    }


}
