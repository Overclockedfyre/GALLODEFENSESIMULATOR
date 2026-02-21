using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrackHealth : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image healthFill;   // assign HPFill image (Image Type = Filled)

    [Header("Health")]
    [SerializeField] private int maxHp = 100;
    [SerializeField] private int currentHp;

    [Header("Lose")]
    [SerializeField] private string loseSceneName = "GameOver";

    private bool gameOver;

    private void Awake()
    {
        currentHp = maxHp;
        Refresh();
    }

    public void TakeDamage(int damage)
    {
        if (gameOver) return;

        damage = Mathf.Max(0, damage);
        currentHp = Mathf.Clamp(currentHp - damage, 0, maxHp);
        Refresh();

        if (currentHp <= 0)
        {
            gameOver = true;
            Time.timeScale = 1f;
            SceneManager.LoadScene(loseSceneName);
        }
    }

    public void Heal(int amount)
    {
        if (gameOver) return;

        amount = Mathf.Max(0, amount);
        currentHp = Mathf.Clamp(currentHp + amount, 0, maxHp);
        Refresh();
    }

    private void Refresh()
    {
        if (healthFill != null)
            healthFill.fillAmount = (maxHp <= 0) ? 0f : (float)currentHp / maxHp;
    }
}