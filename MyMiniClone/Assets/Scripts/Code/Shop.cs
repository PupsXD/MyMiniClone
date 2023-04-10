using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject[] shelves;

    private TomatoPickup tomatoPickup;

    private void Start()
    {
        tomatoPickup = FindObjectOfType<TomatoPickup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Place each picked up tomato on a shelf
            List<GameObject> pickedUpTomatoes = tomatoPickup.GetPickedUpTomatoes();
            for (int i = 0; i < pickedUpTomatoes.Count; i++)
            {
                if (i < shelves.Length)
                {
                    pickedUpTomatoes[i].transform.position = shelves[i].transform.position;
                    pickedUpTomatoes[i].GetComponent<Renderer>().enabled = true;
                }
                else
                {
                    // If there are more picked up tomatoes than shelves, destroy the excess
                    Destroy(pickedUpTomatoes[i]);
                }
            }

            // Clear the list of picked up tomatoes and enable their colliders
            foreach (GameObject tomato in pickedUpTomatoes)
            {
                tomato.GetComponent<Collider>().enabled = true;
            }
            tomatoPickup.ClearPickedUpTomatoes();
        }
    }
}
