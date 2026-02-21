using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrackHealth : MonoBehaviour
{
   public Image healthBar;
   private int Max = 100;
   public int CurrentHp = 100;
   private bool overAttack;


    public void HealthUpdate(int damage)
    {
        
        CurrentHp += damage;
        CurrentHp = Mathf.Clamp(CurrentHp, 0, Max);

        healthBar.fillAmount = (float) CurrentHp / Max;

        if (overAttack == false && CurrentHp <= 0)
            SceneManager.LoadScene("GameOver");

    }
}
