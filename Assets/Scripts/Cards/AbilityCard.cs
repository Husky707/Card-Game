using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCard : Card
{
    public Sprite Img { get; private set; } = null;
    public CardPlayEffect PlayEffect { get; private set;} = null;

    public AbilityCard(AbilityCardData data)
    {
        Name = data.Name;
        Img = data.Graphic;
        PlayEffect = data.PlayEffect;

    }

    public override void Play()
    {
        Debug.Log("Player ability card " + Name);
        ITargetable target = TargetController.CurrnetTarget;
        PlayEffect.Activate(target);
    }
}
