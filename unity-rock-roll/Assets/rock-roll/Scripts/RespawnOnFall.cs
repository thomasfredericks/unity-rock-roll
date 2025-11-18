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

    bool needToRespawn = false;

    bool needToReenableTrail = false;

    void Start()
    {
        // Record the initial position at start
        initialPosition = transform.position + Vector3.up * respawnHeight;
    }

    void FixedUpdate()
    {
        if (needToRespawn)
        {
            Respawn();
        }
        else
        {
            if (needToReenableTrail)
            {
                TrailRenderer tr = GetComponent<TrailRenderer>();
                if (tr != null)
                {
                    tr.emitting = true;
                }
                needToReenableTrail = false;
            }
        }

        // Check if object fell below threshold
        if (transform.position.y < fallThreshold)
        {
            PrepareRespawn();
        }
    }

    void PrepareRespawn()
    {
        if (respawnEnabled)
        {
            TrailRenderer tr = GetComponent<TrailRenderer>();
            if (tr != null)
            {
                tr.emitting = false;
                needToReenableTrail = true;
                //tr.Clear();

                needToRespawn = true;
            }
        }
    }

    void Respawn()
    {
        if (resetVelocityOnRespawn)
        {
            // Reset velocity if object has Rigidbody2D
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }

        // Clear the trail

        // Reset position to initial
        transform.position = initialPosition;

        needToRespawn = false;
    }
}
