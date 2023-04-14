using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFarmTrigger : MonoBehaviour
{
    
    [SerializeField] private MoneyManager moneyManager;
    public int farmPrice = 100;
    public GameObject seller;
    public GameObject poor;
    public GameObject newFarm;

    
   

    public void PurchaseFarm()
    {
        if (moneyManager.currentMoney >= farmPrice)
        {
            Debug.Log("Bebra");
            moneyManager.Purchase(farmPrice);
            newFarm.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Pupa");
            poor.SetActive(true);
        }
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        seller.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        seller.SetActive(false);
    }
}
