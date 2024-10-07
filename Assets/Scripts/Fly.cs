using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints to follow
    public float speed = 7f; // Speed of the fly
    public float verticalRange = 1.5f; // Range of the vertical motion
    public float verticalSpeed = 3f; // Speed of the vertical motion
    public float heightSmoothness = 0.01f; // Smoothness factor for Y position change

    private int currentWaypointIndex = 0; // Current waypoint index
    private Vector3 targetPosition; // Target position to move towards

    void Start()
    {
        if (waypoints.Length > 0)
        {
            targetPosition = waypoints[currentWaypointIndex].position; // Set the initial target position
        }
    }

    void Update()
    {
        // Move towards the current waypoint
        if (waypoints.Length > 0)
        {
            MoveTowardsWaypoint();
        }
    }

    private void MoveTowardsWaypoint()
    {
        // Calculate the desired position
        Vector3 desiredPosition = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Smoothly interpolate the Y position to the target's Y while maintaining the X and Z position
        float newY = Mathf.Lerp(transform.position.y, targetPosition.y + Mathf.Sin(Time.time * verticalSpeed) * verticalRange, heightSmoothness);
        
        transform.position = new Vector3(desiredPosition.x, newY, 0f);

        FlipFly(desiredPosition.x);
        
        // Check if we've reached the waypoint
        if (Vector3.Distance(transform.position, targetPosition) < 0.75f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0; // Loop back to the first waypoint
            }
            targetPosition = waypoints[currentWaypointIndex].position; // Set the next waypoint
        }
    }
    private void FlipFly(float targetX)
    {
        // Check the direction of movement
        if (targetX < transform.position.x)
        {
            // Moving left
            transform.localScale = new Vector3(-1, 1, 1); // Flip horizontally
        }
        else if (targetX > transform.position.x)
        {
            // Moving right
            transform.localScale = new Vector3(1, 1, 1); // Reset to normal
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the fly touches the player
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject, 0.1f); // Make the fly disappear
        }
    }
}
