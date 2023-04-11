using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public MoneyManager moneyManager;
    public TMP_Text moneyText;

    private void Start()
    {
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        moneyText.text = "Money: " + moneyManager.currentMoney.ToString();
    }
}