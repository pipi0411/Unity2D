using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    // Chuyen scene level moi
    public void LevelUI()
    {
        SceneManager.LoadScene("LevelUI");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }
}
