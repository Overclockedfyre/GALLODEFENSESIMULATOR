using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 10f;
    private float health;
    [Header("Reward")]
    [SerializeField] private int moneyDrop = 5;
    public int MoneyDrop => moneyDrop;

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            if (MoneyManager.Instance != null)
                MoneyManager.Instance.Add(moneyDrop);
            else
                Debug.LogError("MoneyManager.Instance is null (no MoneyManager in scene?)");

            Destroy(gameObject);
        }
    }
}