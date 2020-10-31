using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckTester : MonoBehaviour
{
    public bool runTest = true;
    [SerializeField] List<AbilityCardData> cardConfig = null;
    Deck<AbilityCard> testDeck = new Deck<AbilityCard>();

    void Start()
    {
        if (!runTest)
            return;
        Debug.Log("Creating cards...");
        foreach(AbilityCardData data in cardConfig)
            testDeck.Add(new AbilityCard(data));


        PrintDeckOrder(testDeck);

        Debug.Log("Shuffling deck...");
        testDeck.ShuffleDeck();

        PrintDeckOrder(testDeck);

        Debug.Log("Drawing top card...");
        AbilityCard testCard = testDeck.Draw();
        Debug.Log("Drew " + testCard.Name);

        Debug.Log("Playing the card...");
        testCard.Play();
    }


    public void PrintDeckOrder(Deck<AbilityCard> deck)
    {
        Debug.Log("Deck order:");

        int count = deck.Count;
        string nameList = "";
        for(int i = 0; i < count; i++)
        {
            nameList += deck.GetCard(i).Name + ", ";
        }

        Debug.Log(nameList);
    }

}
