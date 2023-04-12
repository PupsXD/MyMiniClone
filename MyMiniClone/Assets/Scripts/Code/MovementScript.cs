using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float speed = 5.0f; // The movement speed of the player
    public Animator anim;

    private void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalAxis, 0.0f, verticalAxis); // Create a movement vector based on the input
        Vector3 newPosition = transform.position + movement * speed * Time.deltaTime; // Calculate the new position

        transform.LookAt(transform.position + movement); // Rotate the player to face the movement direction
        transform.position = newPosition; // Move the player using the Transform component
        
        this.anim.SetFloat("vertical", verticalAxis);
        this.anim.SetFloat("horizontal", horizontalAxis);
        
    }
}