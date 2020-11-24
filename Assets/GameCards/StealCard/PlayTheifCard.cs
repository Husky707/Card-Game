using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTheifEffect", menuName = "Card Effects")]
public class PlayTheifCard : CardPlayEffect
{

    [SerializeField] int StealAmount = 2;
    [SerializeField] AudioClip sound;

    public override void Activate(ITargetable target)
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player == null)
            return;

        player.StealGold(StealAmount);
        AudioSource soundMaker = FindObjectOfType<AudioSource>();
        soundMaker.PlayOneShot(sound);
    }
}
