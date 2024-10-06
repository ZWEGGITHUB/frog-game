using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform player;
    public float yOffset = 50f;

    void Update()
    {
        // Check if the player is assigned
        if (player != null)
        {
            // Get the current position of the follower
            Vector3 newPosition = transform.position;

            // Update the Y value to match the player's Y value
            newPosition.y = player.position.y + yOffset;

            // Apply the updated position to the follower
            transform.position = newPosition;
        }
    }
}
