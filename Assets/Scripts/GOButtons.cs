using UnityEngine;
using UnityEngine.SceneManagement;


public class GOButtons : MonoBehaviour
{
    public void BackButton()
    {
        SoundManagement.Instance.PlayUI(SoundManagement.Instance.ButtonClick);
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitButton()
    {
        SoundManagement.Instance.PlayUI(SoundManagement.Instance.ButtonClick);
        Debug.Log("I wanna go dude");
        Application.Quit();
    }
}
