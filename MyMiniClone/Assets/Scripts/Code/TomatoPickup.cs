using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoPickup : MonoBehaviour
{
    public GameObject tomatoPrefab;
    private GameObject pickedUpTomato;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tomato"))
        {
            // Create a new tomato GameObject in front of the player
            Vector3 spawnPos = transform.position + transform.forward * 2f;
            pickedUpTomato = Instantiate(tomatoPrefab, spawnPos, Quaternion.identity);

            // Disable the tomato collider and renderer so it doesn't interfere with the player
            other.GetComponent<Collider>().enabled = false;
            other.GetComponent<Renderer>().enabled = false;
        }
    }

    private void Update()
    {
        // If the player has picked up the tomato, make it follow them
        if (pickedUpTomato != null)
        {
            pickedUpTomato.transform.position = Vector3.Lerp(pickedUpTomato.transform.position, 
                transform.position + transform.forward * 2f, Time.deltaTime * 10f);
        }
    }
}
