using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void LoadTask1()
    {
        SceneManager.LoadScene("Task1_ClientList");
    }

    public void LoadTask2()
    {
        SceneManager.LoadScene("Task2_DrawingGame");
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
