using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;


public class BotInentory : MonoBehaviour
{
    [SerializeField] Shop shop;
    public int maxTomatoesToTake;
    public int currentTomatoesTaken = 0;
    public bool enoughTomatoes = false;

    public Animator botAnim;
    public bool take = false;
    
    public MoneyManager moneyManager; // Reference to the MoneyManager script
    public MoneyUI moneyUI;

    public TMP_Text counter;
    public GameObject checkmark;

    private void Start()
    {
        maxTomatoesToTake = Random.Range(1, 6);
        counter.text = maxTomatoesToTake.ToString();
        shop = FindObjectOfType<Shop>();

        // Make sure the Shop object was found
        if (shop == null)
        {
            Debug.LogError("Could not find Shop object in the scene!");
        }
        moneyManager = FindObjectOfType<MoneyManager>();
        moneyUI = FindObjectOfType<MoneyUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shop"))
        {
            StartCoroutine(TakeTomatoes());
        }
    }

    IEnumerator TakeTomatoes()
    {
        // Wait until there are enough tomatoes in the shop
        while (shop.tomatosInShop < maxTomatoesToTake)
        {
            yield return null;
        }

        // Take the tomatoes from the shop
        shop.TakeTomatoes(maxTomatoesToTake);
        currentTomatoesTaken = maxTomatoesToTake;
        
    }

    public void Update()
    {
        if (currentTomatoesTaken == maxTomatoesToTake)
        {
            take = true;
            this.botAnim.SetBool("isTaking", take);
            counter.color = new Color(counter.color.r, counter.color.g, counter.color.b, 0);
            checkmark.SetActive(true);
            if (take == true)
            {
                take = false;
                this.botAnim.SetBool("isTaking", take);
                enoughTomatoes = true;
            }
            
        }
        
    }
}
  


