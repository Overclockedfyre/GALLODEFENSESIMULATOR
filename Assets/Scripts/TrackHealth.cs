using UnityEngine;
using UnityEngine.UI;

public class BaseHealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthFill; // drag HPTrack's Image here (usually itself)

    private void Awake()
    {
        if (healthFill == null)
            healthFill = GetComponent<Image>();
    }

    private void OnEnable()
    {
        GameManager.OnBaseHealthChanged += HandleHealthChanged;

        // push an initial refresh if GameManager already exists
        if (GameManager.Instance != null)
            HandleHealthChanged(GameManager.Instance.CurrentHealth, GameManager.Instance.MaxHealth);
    }

    private void OnDisable()
    {
        GameManager.OnBaseHealthChanged -= HandleHealthChanged;
    }

    private void HandleHealthChanged(int current, int max)
    {
        if (healthFill == null) return;
        healthFill.fillAmount = (max <= 0) ? 0f : (float)current / max;
    }
}