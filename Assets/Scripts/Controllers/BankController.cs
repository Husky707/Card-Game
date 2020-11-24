using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using JetBrains.Annotations;

public class BankController : MonoBehaviour
{
    public event Action<ePlayers, int> GoldTotalChanged = delegate { };

    public int GoldToWin => goldToWin;
    [SerializeField] int goldToWin = 20;
    [Header("MoneyText")]
    [SerializeField] TextMeshProUGUI playerMoneyText = null;
    [SerializeField] TextMeshProUGUI aiMoneyText = null;

    [Header("Audio")]
    [SerializeField] AudioSource soundMaker = null;

    public int PlayerGold => playerGold;
    private int playerGold = 0;
    public int AIGold => aiGold;
    private int aiGold = 0;
    /// <summary>
    ///    [Header("Coin Setting")]
    ///    [SerializeField] GameObject Coin = null;
    ///    [SerializeField] float speed = 0.8f;
    /// 
    /// </summary>

    public void PlayerGainGold(int amount)
    {
        SetText(playerMoneyText, playerGold + amount);
        playerGold += amount;
        if(playerGold >= goldToWin)
        {
            GameController.Game.GameOver(ePlayers.player);
        }

        soundMaker.Play();
        GoldTotalChanged(ePlayers.player, playerGold);
    }

    public void AIGainGold(int amount)
    {
        aiGold += amount;
        SetText(aiMoneyText, aiGold);
        if(aiGold >= goldToWin)
        {
            GameController.Game.GameOver(ePlayers.ai);
        }

        soundMaker.Play();
        GoldTotalChanged(ePlayers.ai, aiGold);
    }

    private void SetText(TextMeshProUGUI target, int amount )
    {
        target.text = amount.ToString();
    }


    

}
