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
    public void LevelMenu()
    {
        SceneManager.LoadScene("LevelMenu");
    }
}
