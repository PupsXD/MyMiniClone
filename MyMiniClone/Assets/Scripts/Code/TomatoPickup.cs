using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TomatoPickup : MonoBehaviour
{
    public GameObject tomatoPrefab;
    private List<GameObject> pickedUpTomatoes = new List<GameObject>();
    private TomatoField tomatoField;

    private void Start()
    {
        tomatoField = FindObjectOfType<TomatoField>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tomato") && pickedUpTomatoes.Count < 3)
        {
            // Create a new tomato GameObject in front of the player
            Vector3 spawnPos = transform.position + transform.forward * 2f;
            GameObject newTomato = Instantiate(tomatoPrefab, spawnPos, Quaternion.identity);

            // Add the new tomato to the list of picked up tomatoes and disable its collider and renderer
            pickedUpTomatoes.Add(newTomato);
            other.GetComponent<Collider>().enabled = false;
            other.GetComponent<Renderer>().enabled = false;
            
            // Remove tomato from field
            tomatoField.RemoveTomato(other.gameObject);

            // Position the new tomato in the stack
            newTomato.transform.position = transform.position + transform.forward * (0.5f + pickedUpTomatoes.Count * 0.1f);
            newTomato.transform.rotation = transform.rotation;
        }
    }

    private void Update()
    {
        // Update the position of the stack of picked up tomatoes
        for (int i = 0; i < pickedUpTomatoes.Count; i++)
        {
            pickedUpTomatoes[i].transform.position = transform.position + transform.forward * 2f + transform.up * 0.5f * i;
            pickedUpTomatoes[i].transform.rotation = transform.rotation;
        }
    }
    
    public void ClearPickedUpTomatoes()
    {
        pickedUpTomatoes.Clear();
    }
    public List<GameObject> GetPickedUpTomatoes()
    {
        return pickedUpTomatoes;
    }
}