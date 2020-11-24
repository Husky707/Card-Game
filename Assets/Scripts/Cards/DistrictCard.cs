using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictCard : Card
{
    public Sprite Img { get; private set; } = null;
    public CardPlayEffect PlayEffect { get; private set; } = null;
    public string Discription { get; private set; }

    public DistrictCard(DistrictCardData data)
    {
        Name = data.Name;
        Img = data.Graphic;
        PlayEffect = data.PlayEffect;
        Discription = data.Description;
    }

    public override void Play()
    {
        Debug.Log("Played ability card " + Name);
        ITargetable target = TargetController.CurrnetTarget;
        PlayEffect?.Activate(target);
    }
}
