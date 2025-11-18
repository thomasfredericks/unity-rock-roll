using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOnFall : MonoBehaviour
{
  private Vector3 initialPosition;

    // How far below the screen counts as "falling off"
    public float fallThreshold = -10f; 
    public bool respawnEnabled = true;

    public float respawnHeight = 10f; 
    public bool resetVelocityOnRespawn = true;

    void Start()
    {
        // Record the initial position at start
        initialPosition = transform.position + Vector3.up * respawnHeight;
    }

    void Update()
    {
        // Check if object fell below threshold
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        // Reset position to initial
        transform.position = initialPosition;

    if ( resetVelocityOnRespawn)
        {
             // Reset velocity if object has Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
        }
       
    }
}
