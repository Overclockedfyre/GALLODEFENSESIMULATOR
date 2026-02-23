using UnityEngine;
using UnityEngine.SceneManagement;

public class WButton : MonoBehaviour
{
   public void HomeButton()
    {
        Debug.Log("back");
        SoundManagement.Instance.PlayUI(SoundManagement.Instance.ButtonClick);
        SceneManager.LoadScene("MainMenu");
    }

    public void LeaveButton()
    {
        Debug.Log("exit");
        SoundManagement.Instance.PlayUI(SoundManagement.Instance.ButtonClick);
        Application.Quit();
    }
}
