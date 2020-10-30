using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictCard : Card
{
    public Sprite Img { get; private set; } = null;
    public CardPlayEffect PlayEffect { get; private set; } = null;

    public DistrictCard(DistrictCardData data)
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
