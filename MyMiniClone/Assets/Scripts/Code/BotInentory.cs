using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotInentory : MonoBehaviour
{
    public Shop shop;
    public int maxTomatoesToTake;
    private int currentTomatoesTaken = 0;
    public bool enoughTomatoes = false;

    public Animator botAnim;
    public bool take = false;

    private void Start()
    {
        maxTomatoesToTake = Random.Range(1, 6);
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
            if (take == true)
            {
                take = false;
                this.botAnim.SetBool("isTaking", take);
                enoughTomatoes = true;
            }
            
        }
    }
}
  


