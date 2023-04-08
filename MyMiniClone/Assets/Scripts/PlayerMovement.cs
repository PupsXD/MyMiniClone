using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // The movement speed of the player

    private Rigidbody rb; // CHANGE THAT TO PUBLIC LATER

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical"); 

        Vector3 movement = new Vector3(horizontal, 0.0f, vertical); // Create a movement vector based on the input
        Vector3 newPosition = transform.position + movement * speed * Time.deltaTime; // Calculate the new position

        rb.MovePosition(newPosition); // Move the player using the MovePosition method of the Rigidbody component
    }
}
