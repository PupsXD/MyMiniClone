using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Bot : MonoBehaviour
{
    public List<GameObject> destinationTriggers; // List of triggers for the bot to visit
    public GameObject checkoutTrigger; // The checkout trigger
    public GameObject finishTrigger; // The finish trigger
    public BotInentory botInventory; // The BotInventory component

    public bool waiter = false;
    public bool walker = true;
    public Animator botAnim;

    private UnityEngine.AI.NavMeshAgent agent;
    private int currentDestinationIndex = 0;
    
    //Price of a Tomato
    public int tomatoPrice = 10;
    
    

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        GoToNextDestination();
    }

    private void GoToNextDestination()
    {
        if (currentDestinationIndex < destinationTriggers.Count)
        {
            agent.SetDestination(destinationTriggers[currentDestinationIndex].transform.position);
        }
        else if (checkoutTrigger != null)
        {
            agent.SetDestination(checkoutTrigger.transform.position);
        }
        else if (finishTrigger != null)
        {
            agent.SetDestination(finishTrigger.transform.position);
        }
        else
        {
            Destroy(gameObject);
        }

        currentDestinationIndex++;
    }

    private void Update()
    {
        if (walker != true)
        {
            this.botAnim.SetBool("isWalking", walker);
        }
        else
        {
            this.botAnim.SetBool("isWalking", walker);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the bot has reached its current destination
        if (currentDestinationIndex > 0 && currentDestinationIndex - 1 < destinationTriggers.Count && other.gameObject == destinationTriggers[currentDestinationIndex - 1])
        {
            if (other.CompareTag("Shop"))
            {
                walker = false;
                
                //waiter = true;
                //this.botAnim.SetBool("isWaiting", waiter);
                StartCoroutine(WaitForTomatoes());
            }
            else
            {
                GoToNextDestination();
            }
        }
        // Check if the bot has reached the checkout trigger
        else if (other.gameObject == checkoutTrigger)
        {
            // Give the player money for each tomato the bot has
            int moneyToAdd = botInventory.currentTomatoesTaken * tomatoPrice;
            FindObjectOfType<MoneyManager>().AddMoney(moneyToAdd);
            FindObjectOfType<MoneyUI>().UpdateMoneyText();
            Debug.Log("Trigger Entered");

            // Set the bot's destination to the finish trigger
            agent.SetDestination(finishTrigger.transform.position);
        }
        // Check if the bot has reached the finish trigger
        else if (other.gameObject == finishTrigger)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Bot has been destroyed");
    }

    private IEnumerator WaitForTomatoes()
    {
        yield return new WaitUntil(() => botInventory.enoughTomatoes);
        //waiter = false;
       // this.botAnim.SetBool("isWaiting", waiter);
        walker = true;
        
        GoToNextDestination();
    }
}