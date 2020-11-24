using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandDisplay : MonoBehaviour
{
    public event Action<Card> PlayRequest = delegate { };

    [SerializeField] GameObject CardPrefab = null;
    [SerializeField] int maxCards = 5;
    [SerializeField] RectTransform cardPosition = null;

    private Deck<Card> Hand = null;
    private Dictionary<Card, GameObject> HandLookup = new Dictionary<Card, GameObject>();

    public void SetTergetDeck<T>(Deck<T> deck) where T : Card
    {
        if (Hand != null)
            DestroyHand();

        if (deck == null)
        {
            Hand = null;
            return;
        }

        Hand = deck as Deck<Card>;
        GetCurrentCards(deck);
        OpenDeckEvents();
        OpenCardEvents();
    }

    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////
    #region Events 
    private void OnEnable()
    {
        if (Hand == null)
            return;

        OpenDeckEvents();
        OpenCardEvents();
    }

    private void OnDisable()
    {
        if (Hand == null)
            return;

        CloseDeckEvents();
        CloseCardEvents();
    }

    private void OpenDeckEvents()
    {
        if (Hand == null)
            Debug.Log("Hand is null");
        Hand.CardAdded += SetCard;
        Hand.CardRemoved += CardRemoved;
    }

    private void CloseDeckEvents()
    {
        Hand.CardAdded -= SetCard;
        Hand.CardRemoved -= CardRemoved;
    }

    private void OpenCardEvent(CardObj target)
    {
        target.PlayRequest += OnCardPlayRequest;
    }
    private void CloseCardEvent(CardObj target)
    {
        target.PlayRequest -= OnCardPlayRequest;
    }
    private void OpenCardEvents()
    {
        foreach(GameObject obj in HandLookup.Values)
        {
            CardObj card = obj.GetComponent<CardObj>();
            if (card == null)
                return;
            OpenCardEvent(card);
        }
    }
    private void CloseCardEvents()
    {
        foreach (GameObject obj in HandLookup.Values)
        {
            CardObj card = obj.GetComponent<CardObj>();
            if (card == null)
                return;
            CloseCardEvent(card);
        }
    }

    #endregion

    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////
    private void SetCard<T>(T card) where T : Card
    {
        if (HandLookup.ContainsKey(card))
            return;

        GameObject newCard = Instantiate(CardPrefab, cardPosition);
        HandLookup.Add(card, newCard);
        CardObj newcardobj = newCard.GetComponent<CardObj>();
        if (newcardobj == null) return;

        newcardobj.InitCard(Hand, card);
        OpenCardEvent(newcardobj);
    }

    private void CardRemoved<T>(T card) where T : Card
    {
        if (!HandLookup.ContainsKey(card))
            return ;

        GameObject obj = HandLookup[card];
        HandLookup.Remove(card);

        DestroyCard(obj);
    }

    private void DestroyCard(GameObject cardObj)
    {
        CardObj obj = cardObj.GetComponent<CardObj>();
        if (obj != null)
            CloseCardEvent(obj);

        Destroy(cardObj);
    }

    private void DestroyHand()
    {
        CloseCardEvents();
        CloseDeckEvents();

        GameObject[] objs = new GameObject[HandLookup.Count];
        int j = 0;
        foreach(GameObject o in HandLookup.Values)
        {
            objs[j] = o;
            j++;
        }
        for(int i = 0; i < objs.Length; i++)
        {
            Destroy(objs[i]);
        }
    }

    private void GetCurrentCards<T>(Deck<T> fromDeck) where T : Card
    {
        if (Hand == null)
            return;

        int count = fromDeck.Count;
        for(int i = 0; i < count; i++)
        {
            SetCard<Card>(fromDeck.GetCard(i));
        }
    }

    public void HideDisplay()
    {
        SetDisplay(false);
    }

    public void ShowDisplay()
    {
        SetDisplay(true);
    }

    private void SetDisplay(bool toState)
    {
        foreach (GameObject each in HandLookup.Values)
        {
            each.SetActive(toState);
        }
    }

    private void OnCardPlayRequest(Card requestingCard)
    {
        PlayRequest.Invoke(requestingCard);
    }
}
