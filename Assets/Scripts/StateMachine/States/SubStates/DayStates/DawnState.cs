using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DawnState : DayState
{

    [SerializeField] GameObject DawnUI = null;
    [SerializeField] Button endDawnButton = null;

    [SerializeField] PlayerController player = null;
    [SerializeField] List<DistrictCardData> availableCards = new List<DistrictCardData>();
    [SerializeField] AudioClip soundBite = null;

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

        player.playerHand.Add(new DistrictCard(GetRandomCard()));
        player.playerHand.Add(new DistrictCard(GetRandomCard()));
        player.playerHand.Add(new DistrictCard(GetRandomCard()));
        AudioSource sound = FindObjectOfType<AudioSource>();
        sound?.PlayOneShot(soundBite);
    }

    private DistrictCardData GetRandomCard()
    {
        return availableCards[UnityEngine.Random.Range(0, availableCards.Count)];
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

    float waitCount = 0f;
    public override void Tick()
    {
        base.Tick();
        waitCount += Time.deltaTime;
        if(waitCount >= 3f)
        {
            waitCount = 0;
            EndDawnState();
        }
    }
}
