using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene")]
    public string firstLevelSceneName = "Level1";

    [Header("Panels")]
    public GameObject mainPanel;

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevelSceneName);
    }

   

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}