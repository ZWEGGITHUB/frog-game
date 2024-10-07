using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScore : MonoBehaviour
{
    public int scoreValue = 1000;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreValue); 
            }

            Destroy(gameObject);
        }
    }
}
