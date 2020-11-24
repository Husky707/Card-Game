using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardObj : MonoBehaviour
{

    public event Action<Card> PlayRequest = delegate { };

    [SerializeField] Button PlayButton = null;
    [SerializeField] TextMeshProUGUI NameText = null;
    [SerializeField] TextMeshProUGUI DescText = null;
    public Deck<Card> myDeck = null;
    public Card myCard= null;

    public void InitCard(Deck<Card> deck, Card card)
    {
        myDeck = deck;
        myCard = card;
        NameText.text = myCard.Name;
        DistrictCard dCard = (DistrictCard)card;
        if (dCard != null)
         DescText.text = dCard.Discription;
    }

    #region Init
    private void OnEnable()
    {
        PlayButton.onClick.AddListener(OnPlay);
    }

    private void OnDisable()
    {
        PlayButton.onClick.RemoveListener(OnPlay);

    }
    #endregion

    private void OnPlay()
    {

        PlayRequest.Invoke(myCard);
    }

}
