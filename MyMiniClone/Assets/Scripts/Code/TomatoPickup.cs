using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TomatoPickup : MonoBehaviour
{
    public GameObject tomatoPrefab;
    public List<GameObject> pickedUpTomatoes = new List<GameObject>();
    private TomatoField tomatoField;
    public Animator anim;

    public bool isHolding;

    public int maxTomatoes = 3;
    
    public GameObject playerUI;

    private void Start()
    {
        tomatoField = FindObjectOfType<TomatoField>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tomato") && pickedUpTomatoes.Count < maxTomatoes)
        {
            // Create a new tomato GameObject in front of the player
            Vector3 spawnPos = transform.position + transform.forward * 1.5f;
            GameObject newTomato = Instantiate(tomatoPrefab, spawnPos, Quaternion.identity);

            // Add the new tomato to the list of picked up tomatoes and disable its collider and renderer
            pickedUpTomatoes.Add(newTomato);
            other.GetComponent<Collider>().enabled = false;
            other.GetComponent<Renderer>().enabled = false;

            // Remove tomato from field
            tomatoField.RemoveTomato(other.gameObject);

            // Position the new tomato in the stack
            newTomato.transform.position = transform.position + transform.forward * (1.5f + pickedUpTomatoes.Count * 0.1f);
            newTomato.transform.rotation = transform.rotation;

            //calling animation in MovementScript
            isHolding = true;
            this.anim.SetBool("holding", isHolding);
        }
    }

    private void Update()
    {
        // Update the position of the stack of picked up tomatoes
        for (int i = 0; i < pickedUpTomatoes.Count; i++)
        {
            pickedUpTomatoes[i].transform.position = transform.position + transform.forward * 1.5f + transform.up * 0.5f * i;
            pickedUpTomatoes[i].transform.rotation = transform.rotation;
        }

        if (pickedUpTomatoes.Count.Equals(0))
        {
            isHolding = false;
            this.anim.SetBool("holding", isHolding);
        }

        if (pickedUpTomatoes.Count.Equals(maxTomatoes))
        {
            playerUI.SetActive(true);
        }
        else
        {
            playerUI.SetActive(false);
        }
    }

    public void ClearPickedUpTomatoes()
    {
        pickedUpTomatoes.Clear();
    }

    public void RemovePickedUpTomato(GameObject tomato)
    {
        if (pickedUpTomatoes.Contains(tomato))
        {
            // Remove the tomato from the list of picked up tomatoes
            pickedUpTomatoes.Remove(tomato);

            // Enable the tomato's collider and renderer
            tomato.GetComponent<Collider>().enabled = true;
            tomato.GetComponent<Renderer>().enabled = true;

            

            // Update the position of the remaining tomatoes
            for (int i = 0; i < pickedUpTomatoes.Count; i++)
            {
                pickedUpTomatoes[i].transform.position = transform.position + transform.forward * 1.5f + transform.up * 0.5f * i;
                pickedUpTomatoes[i].transform.rotation = transform.rotation;
            }
        }

        if (pickedUpTomatoes.Count.Equals(0))
        {
            isHolding = false;
            this.anim.SetBool("holding", isHolding);
        }
    }
    
    public void SetPickedUpTomatoes(List<GameObject> tomatoes)
    {
        pickedUpTomatoes = tomatoes;
    }

    public List<GameObject> GetPickedUpTomatoes()
    {
        return pickedUpTomatoes;
    }
}
