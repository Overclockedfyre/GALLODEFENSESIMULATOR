using UnityEngine;
using UnityEngine.SceneManagement;

public class GPButtons : MonoBehaviour
{
    public GameObject PauseMenu;
    public int damageAmount = 10;

    public void SetButton() // Pause
    {
        SoundManagement.Instance.PlayUI(SoundManagement.Instance.ButtonClick);
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;   // freeze game
    }

    public void ResumeButton()
    {
        SoundManagement.Instance.PlayUI(SoundManagement.Instance.ButtonClick);
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;   // unfreeze game
    }

    public void HomeButton()
    {
        SoundManagement.Instance.PlayUI(SoundManagement.Instance.ButtonClick);
        Time.timeScale = 1f;   // IMPORTANT: reset before loading scene
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitButton()
    {
        SoundManagement.Instance.PlayUI(SoundManagement.Instance.ButtonClick);
        Debug.Log("time to leave");
        Application.Quit();
    }

    public void DealDamage()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("No GameManager in scene.");
            return;
        }

        GameManager.Instance.DamageBase(damageAmount);
    }
}   
