using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    public float speed = 5.0f; // The movement speed of the player
    public Animator anim;

    private Vector2 movementInput; // The movement input vector

    public ParticleSystem dust;

    private void Update()
    {
        Vector3 movement = new Vector3(movementInput.x, 0.0f, movementInput.y); // Create a movement vector based on the input
        Vector3 newPosition = transform.position + movement * speed * Time.deltaTime; // Calculate the new position
        
        PlayDirt();

        transform.LookAt(transform.position + movement); // Rotate the player to face the movement direction
        transform.position = newPosition; // Move the player using the Transform component
        
        this.anim.SetFloat("vertical", movementInput.y);
        this.anim.SetFloat("horizontal", movementInput.x);
    }

    private void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>(); // Get the movement input vector from the Input System
    }

    private void PlayDirt()
    {
        dust.Play();
    }
}