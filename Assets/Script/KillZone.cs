using UnityEngine;

public class KillZone : MonoBehaviour
{
    private GameManager gameManager; // Reference to the GameManager script
    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>(); // Find the GameManager in the scene
    }
    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Enemy"))
    {
        Debug.Log("Enemy đã bị tiêu diệt");
        Destroy(other.gameObject); // enemy bị tiêu diệt
        gameManager.AddScore(1); // tăng điểm số
    }

    if (other.CompareTag("Player"))
    {
        Debug.Log("Player đã bị tiêu diệt");
        gameManager.GameOver(); // Gọi hàm GameOver trong GameManager
        Destroy(other.gameObject); // player bị tiêu diệt
    }
}
}
