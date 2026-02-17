using UnityEngine;
using UnityEngine.SceneManagement;


public class GOButtons : MonoBehaviour
{
    public void BackButton()
    {
       SceneManager.LoadScene("MainMenu");
    }

    public void ExitButton()
    {
        Debug.Log("I wanna go dude");
        Application.Quit();
    }
}
