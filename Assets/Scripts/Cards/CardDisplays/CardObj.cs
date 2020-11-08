using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardObj : MonoBehaviour
{

    [SerializeField] Button PlayButton = null;
    [SerializeField] TextMeshProUGUI NameText = null;
    [SerializeField] TextMeshProUGUI DescText = null;
    public Deck<DistrictCard> myDeck = null;
    public DistrictCard myCard= null;
    public int myIndex = 0;

    public void InitCard(Deck<DistrictCard> deck, DistrictCard card, int handIndex)
    {
        myDeck = deck;
        myCard = card;
        myIndex = handIndex;
        NameText.text = myCard.Name;
        DescText.text = myCard.Discription;
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
        myDeck?.Remove(myIndex);
        myCard?.Play();
    }

}
