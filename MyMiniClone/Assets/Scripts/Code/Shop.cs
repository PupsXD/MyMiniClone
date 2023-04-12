using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<GameObject> shelves = new List<GameObject>();
    public int tomatosInShop;
    private TomatoPickup tomatoPickup;
    public List<GameObject> usedShelves = new List<GameObject>();
    

    private void Start()
    {
        tomatoPickup = FindObjectOfType<TomatoPickup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Place each picked up tomato on an unused shelf
            List<GameObject> pickedUpTomatoes = tomatoPickup.GetPickedUpTomatoes();

            for (int i = 0; i < pickedUpTomatoes.Count; i++)
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
                    pickedUpTomatoes[i].transform.position = unusedShelf.transform.position;
                    pickedUpTomatoes[i].GetComponent<Renderer>().enabled = true;
                    tomatosInShop += 1;

                    // Add the shelf to the list of used shelves
                    usedShelves.Add(unusedShelf);
                }
                else
                {
                    // If there are no unused shelves, disable the tomato's collider so that it stays in the player's hands
                    pickedUpTomatoes[i].GetComponent<Collider>().enabled = false;
                }
            }

            // Clear the list of picked up tomatoes
            tomatoPickup.ClearPickedUpTomatoes();
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
                shelfToRemove.SetActive(false);
            }
        }
    }
}