using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GainGold")]
public class PlayGoldCard : CardPlayEffect
{
    [SerializeField] int goldAmount = 3;
    public override void Activate(ITargetable target)
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if(player == null)
        {
            Debug.Log("Couldn't find player to give them gold");
            return;
        }

        player.GainGold(goldAmount);
    }
}
