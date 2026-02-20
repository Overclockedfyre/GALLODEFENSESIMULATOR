using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    [Header("Money")]
    [SerializeField] private int startingMoney = 100;
    public int Money { get; private set; }

    [Header("UI")]
    [SerializeField] private TMP_Text moneyText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Money = startingMoney;
        RefreshUI();
    }

    public void Add(int amount)
    {
        Money += Mathf.Max(0, amount);
        RefreshUI();
    }

    public bool TrySpend(int amount)
    {
        amount = Mathf.Max(0, amount);
        if (Money < amount) return false;

        Money -= amount;
        RefreshUI();
        return true;
    }

    private void RefreshUI()
    {
        if (moneyText != null)
            moneyText.text = $"$ {Money}";
    }
}