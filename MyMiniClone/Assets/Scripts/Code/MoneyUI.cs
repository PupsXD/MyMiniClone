using System;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public MoneyManager moneyManager;
    public TMP_Text moneyText;
    //public int uiMoney;

    private void Start()
    {
        UpdateMoneyText();
        //uiMoney = moneyManager.currentMoney;
    }

    private void Update()
    {
        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
        moneyText.text = "Money: " + moneyManager.currentMoney.ToString();
    }
}