using UnityEngine;
using UnityEngine.SceneManagement;

public class GPButtons : MonoBehaviour
{
    public GameObject PauseMenu;
    public void SetButton()
    {
        PauseMenu.SetActive(true);
    }
    public void ResumeButton()
    {
        PauseMenu.SetActive(false);

    }
    public void HomeButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitButton()
    {
        Debug.Log("time to leave");
        Application.Quit();
    }

}
