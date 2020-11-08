using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDisplay : MonoBehaviour
{

    [SerializeField] GameObject CardPrefab = null;
    [SerializeField] int maxCards = 5;
    [SerializeField] RectTransform cardPosition = null;

    private Deck<DistrictCard> Hand = new Deck<DistrictCard>();
    private Dictionary<DistrictCard, GameObject> HandLookup = new Dictionary<DistrictCard, GameObject>();

    private void OnEnable()
    {
        Hand.CardAdded += SetCard;
        Hand.CardRemoved += CardRemoved;
    }

    private void OnDisable()
    {
        Hand.CardAdded -= SetCard;
        Hand.CardRemoved -= CardRemoved;
    }

    private void SetCard<T>(T card) where T : DistrictCard
    {
        if (HandLookup.ContainsKey(card))
            return;

        GameObject newCard = Instantiate(CardPrefab, cardPosition);
        HandLookup.Add(card, newCard);
        CardObj newcardobj = newCard.GetComponent<CardObj>();
        if (newcardobj == null) return;

        newcardobj.InitCard(Hand, card, Hand.Count -1);
    }

    private void CardRemoved<T>(T card) where T : DistrictCard
    {
        if (!HandLookup.ContainsKey(card))
            return ;

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
}
