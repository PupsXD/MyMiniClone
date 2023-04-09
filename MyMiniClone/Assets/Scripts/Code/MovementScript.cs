using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float speed = 5.0f; // The movement speed of the player

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0.0f, vertical); // Create a movement vector based on the input
        Vector3 newPosition = transform.position + movement * speed * Time.deltaTime; // Calculate the new position

        transform.LookAt(transform.position + movement); // Rotate the player to face the movement direction
        transform.position = newPosition; // Move the player using the Transform component
    }
}