using UnityEngine;
using UnityEngine.SceneManagement;


public class MMButtons : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("GamePlay");
        Debug.Log("I pressed it");
    }
    public void ExitButton()
    {
        Debug.Log("let me go");
        Application.Quit();
    }
}
