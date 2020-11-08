using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySetupState : PlayState
{
    [SerializeField] GameObject GameCanvas = null;
    [SerializeField] FieldBuilder fieldBuilder = null;
    [SerializeField] Deck<DistrictCard> playerDeck = null;

    [Header("Cards for deck")]
    [SerializeField] DistrictCard[] cards = null;

    public override void Enter()
    {
        SetUpGame();

        //After Setup
        base.Enter();
        StateMachine.ChangeState<PlayDayState>();
    }

    private void SetUpGame()
    {
        GameCanvas?.SetActive(true);
        //fieldBuilder?.GenerateField();


        //CreateDecks();
    }

    private void CreateDecks()
    {
        foreach (DistrictCard card in cards)
        {
            playerDeck.Add(card);
            playerDeck.Add(card);
            playerDeck.Add(card);
            playerDeck.Add(card);
        }

        playerDeck.ShuffleDeck();
    }
}
