using UnityEngine;
using UnityEngine.SceneManagement;


public class MMButtons : MonoBehaviour
{
    public void PlayButton()
    {
        Debug.Log("I pressed it");
        SoundManagement.Instance.PlayUI(SoundManagement.Instance.ButtonClick);
        SceneManager.LoadScene("GamePlay");

    }
    public void ExitButton()
    {
        Debug.Log("let me go");
        SoundManagement.Instance.PlayUI(SoundManagement.Instance.ButtonClick);
        Application.Quit();
    }
}
