using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Base Health")]
    [SerializeField] private int maxHealth = 20;
    [SerializeField] private int currentHealth;

    [Header("UI (optional)")]
    [SerializeField] private TMP_Text healthText; // drag a TMP text here (optional)

    [Header("Lose Screen")]
    [SerializeField] private string loseSceneName = "Lose"; // set to your lose scene name

    private bool gameOver;

    private void Awake()
    {
        // Simple singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        currentHealth = maxHealth;
        UpdateUI();
    }

    public void DamageBase(int amount)
    {
        if (gameOver) return;

        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        UpdateUI();

        if (currentHealth <= 0)
        {
            gameOver = true;
            LoseGame();
        }
    }

    private void UpdateUI()
    {
        if (healthText != null)
            healthText.text = $"HP: {currentHealth}/{maxHealth}";
    }

    private void LoseGame()
    {
        Time.timeScale = 1f; // important in case you paused
        SceneManager.LoadScene(loseSceneName);
    }
}