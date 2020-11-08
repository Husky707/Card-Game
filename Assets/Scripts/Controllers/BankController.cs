using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class BankController : MonoBehaviour
{
    [SerializeField] int goldToWin = 20;
    [Header("MoneyText")]
    [SerializeField] TextMeshProUGUI playerMoneyText = null;
    [SerializeField] TextMeshProUGUI aiMoneyText = null;


    private int playerGold = 0;
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
    }

    public void AIGainGold(int amount)
    {
        aiGold += amount;
        SetText(aiMoneyText, aiGold);
        if(aiGold >= goldToWin)
        {
            GameController.Game.GameOver(ePlayers.ai);
        }
    }

    private void SetText(TextMeshProUGUI target, int amount )
    {
        target.text = amount.ToString();
    }


}
