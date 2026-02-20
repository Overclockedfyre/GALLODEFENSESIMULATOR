using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 10f;
    private float health;

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
            Destroy(gameObject);
    }
}