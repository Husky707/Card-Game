using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DeckObj : MonoBehaviour
{
    [SerializeField] DistrictCardData cardData = null;


    private void OnMouseDown()
    {

        PlayerController player = FindObjectOfType<PlayerController>();
        if(player == null)
        {
            Debug.Log("Couldn't find a player");
            return;
        }

        Deck<Card> ToDeck = player.playerHand;
        if(ToDeck == null)
        {
            Debug.Log("Could not find player's deck");
            return;
        }

        ToDeck.Add(new DistrictCard(cardData));
        Debug.Log("Added a card to player's hand");

    }
}
