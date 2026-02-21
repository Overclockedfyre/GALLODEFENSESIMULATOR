using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event Action<int, int> OnBaseHealthChanged; // current, max

    [Header("Base Health")]
    [SerializeField] private int maxHealth = 20;
    private int currentHealth;

    [Header("UI (optional)")]
    [SerializeField] private TMP_Text healthText; // optional

    [Header("Lose Screen")]
    [SerializeField] private string loseSceneName = "Lose";

    private bool gameOver;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        currentHealth = maxHealth;
        PublishHealth();
    }

    public void DamageBase(int amount)
    {
        if (gameOver) return;

        amount = Mathf.Max(0, amount);
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        PublishHealth();

        if (currentHealth <= 0)
        {
            gameOver = true;
            LoseGame();
        }
    }

    public void HealBase(int amount)
    {
        if (gameOver) return;

        amount = Mathf.Max(0, amount);
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        PublishHealth();
    }

    private void PublishHealth()
    {
        if (healthText != null)
            healthText.text = $"HP: {currentHealth}/{maxHealth}";

        OnBaseHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    private void LoseGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(loseSceneName);
    }
}