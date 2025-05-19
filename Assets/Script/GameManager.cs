using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText; 
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWinUi;
    private bool isGameOver = false; 
    private bool isGameWin = false;
    void Start()
    {
        UpdateScoreText(); 
        gameOverPanel.SetActive(false); 
        gameWinUi.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore(int points)
    {
        if (!isGameOver && !isGameWin) // Check if the game is not over
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
    public void GameWin()
    {
        isGameWin = true; // Set the game win flag to true
        score = 0;
        Time.timeScale = 0; // Pause the game
        gameWinUi.SetActive(true); // Activate the game win UI
    }
    public void RestartGame()
    {
        Time.timeScale = 1; 
        isGameOver = false; 
        score = 0; 
        UpdateScoreText(); 
        SceneManager.LoadScene("Game"); 
    }
    public void MainMenu()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene("Menu"); 
    }
    public bool IsGameOver()
    {
        return isGameOver; // Return the game over flag
    }
    public bool IsGameWin()
    {
        return isGameWin; // Return the game win flag
    }
}
