using UnityEngine;
using UnityEngine.SceneManagement;

public class GPButtons : MonoBehaviour
{
    public GameObject PauseMenu;

    public void SetButton() // Pause
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;   // freeze game
    }

    public void ResumeButton()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;   // unfreeze game
    }

    public void HomeButton()
    {
        Time.timeScale = 1f;   // IMPORTANT: reset before loading scene
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitButton()
    {
        Debug.Log("time to leave");
        Application.Quit();
    }
}
