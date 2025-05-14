using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager; // Reference to the GameManager script
    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>(); // Find the GameManager in the scene
    }
    private void  OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject); // Destroy the coin object
            gameManager.AddScore(1);
        }
        else if (other.CompareTag("Trap"))
        {
            gameManager.GameOver(); // Call the GameOver method in the GameManager
            Destroy(gameObject); // Destroy the player object
        }
    }
}
