using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager; 
    private AudioManager audioManager;
    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            audioManager.PlayCoinSound();
            gameManager.AddScore(1);
        }
        else if (other.CompareTag("Trap"))
        {

            GetComponent<PlayerController>().TakeDamage(1);
        }
        else if (other.CompareTag("Key"))
        {
            gameManager.GameWin();
            Destroy(other.gameObject);
        }
    }
}
