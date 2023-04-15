using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<GameObject> shelves = new List<GameObject>();
    public int tomatosInShop;
    private TomatoPickup tomatoPickup;
    public List<GameObject> usedShelves = new List<GameObject>();
    
    public TMP_Text tomatoAmount;

    private void Start()
    {
        tomatoPickup = FindObjectOfType<TomatoPickup>();
    }

    private void Update()
    {
        tomatoAmount.text = tomatosInShop.ToString();
    }

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        // Place each picked up tomato on an unused shelf
        List<GameObject> pickedUpTomatoes = tomatoPickup.GetPickedUpTomatoes();

        int numTomatoesToPlace = Mathf.Min(pickedUpTomatoes.Count, GetUnusedShelves().Count);

        for (int i = 0; i < numTomatoesToPlace; i++)
        {
            if (tomatosInShop >= shelves.Count)
            {
                // If the shop is already at maximum capacity, disable the tomato's collider so that it stays in the player's hands
                pickedUpTomatoes[i].GetComponent<Collider>().enabled = false;
                Debug.Log("Full");
            }
            else
            {
                // Find an unused shelf
                GameObject unusedShelf = null;
                foreach (GameObject shelf in shelves)
                {
                    if (!usedShelves.Contains(shelf))
                    {
                        unusedShelf = shelf;
                        break;
                    }
                }

                // If there are unused shelves, place the tomato on an unused shelf
                if (unusedShelf != null)
                {
                    // Assign the tomato as a child of the used shelf
                    pickedUpTomatoes[i].transform.parent = unusedShelf.transform;
                    pickedUpTomatoes[i].transform.localPosition = Vector3.zero;
                    pickedUpTomatoes[i].GetComponent<Renderer>().enabled = true;
                    tomatosInShop += 1;

                    // Add the shelf to the list of used shelves
                    usedShelves.Add(unusedShelf);
                }
                else
                {
                    // If there are no unused shelves, disable the tomato's collider so that it stays in the player's hands
                    pickedUpTomatoes[i].GetComponent<Collider>().enabled = false;
                    Debug.Log("Full");
                }
            }
        }

        // Remove the placed tomatoes from the pickedUpTomatoes list
        pickedUpTomatoes.RemoveRange(0, numTomatoesToPlace);

        // Clear the remaining picked up tomatoes
        tomatoPickup.SetPickedUpTomatoes(pickedUpTomatoes);
    }
}


    public List<GameObject> GetUnusedShelves()
    {
        List<GameObject> unusedShelves = new List<GameObject>();
        foreach (GameObject shelf in shelves)
        {
            if (!usedShelves.Contains(shelf))
            {
                unusedShelves.Add(shelf);
            }
        }
        return unusedShelves;
    }


    public void AddUsedShelf(GameObject shelf)
    {
        usedShelves.Remove(shelf);
    }
    
    public void TakeTomatoes(int numTomatoes)
    {
        tomatosInShop -= numTomatoes;

        // Remove used shelves based on the number of taken tomatoes
        for (int i = 0; i < numTomatoes; i++)
        {
            if (usedShelves.Count > 0)
            {
                GameObject shelfToRemove = usedShelves[0];
                usedShelves.RemoveAt(0);

                // Remove the tomatoes from the shelf
                foreach (Transform child in shelfToRemove.transform)
                {
                    Destroy(child.gameObject);
                }

                // Reactivate the shelf so that the player can replace the taken tomatoes
                shelfToRemove.SetActive(true);
            }
        }
    }
}