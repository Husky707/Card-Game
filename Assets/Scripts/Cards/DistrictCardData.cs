using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDistrictCard", menuName = "CardData/DistrictCard")]

public class DistrictCardData : ScriptableObject
{
    [SerializeField] string _name = "...";
    public string Name => _name;

    [SerializeField] Sprite _graphic = null;
    public Sprite Graphic => _graphic;

    [SerializeField] CardPlayEffect _playEffect = null;
    public CardPlayEffect PlayEffect => _playEffect;
}
