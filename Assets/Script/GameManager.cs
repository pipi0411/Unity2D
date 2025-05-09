using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component for displaying the score
    [SerializeField] private GameObject gameOverPanel; // Reference to the GameObject for the game over panel
    private bool isGameOver = false; // Flag to check if the game is over
    void Start()
    {
        UpdateScoreText(); // Initialize the score text display
        gameOverPanel.SetActive(false); // Hide the game over panel at the start
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore(int points)
    {
        if (!isGameOver) // Check if the game is not over
        {
            score += points; // Add points to the score
            UpdateScoreText(); // Update the score text display
        }
    }
    private void UpdateScoreText()
    {
        scoreText.text = score.ToString(); // Update the score text with the current score
    }
    public void GameOver()
    {
        isGameOver = true; // Set the game over flag to true
        score = 0; // Reset the score to 0
        Time.timeScale = 0; // Pause the game
        gameOverPanel.SetActive(true); // Activate the game over panel
    }
    public void RestartGame()
    {
        isGameOver = false; // Reset the game over flag
        score = 0; // Reset the score to 0
        UpdateScoreText(); // Update the score text display
        Time.timeScale = 1; // Resume the game
        SceneManager.LoadScene("Game"); // Reload the current scene
    }
    public bool IsGameOver()
    {
        return isGameOver; // Return the game over flag
    }
}
