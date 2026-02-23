using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UPButtons : MonoBehaviour
{
    [Header("Price Text References")]
    [SerializeField] private TMP_Text damagePriceText;
    [SerializeField] private TMP_Text bulletSpeedPriceText;
    [SerializeField] private TMP_Text fireRatePriceText;
    private int damageUpgradeCost = 300;
    private int bulletSpeedUpgradeCost = 300;
    private int firerateUpgradeCost = 300;
    private void Start()
    {
        UpdatePriceUI();
    }

    public void damageUP()
    {
        if (!MoneyManager.Instance.TrySpend(damageUpgradeCost))
        {
            SoundManagement.Instance.PlayUI(SoundManagement.Instance.DontBuyGallo);           
            return;
        }

        SoundManagement.Instance.PlayUI(SoundManagement.Instance.BuyGallo);
        Projectile projectile = FindFirstObjectByType<Projectile>();
        if (projectile != null)
        {
            projectile.damage += 1;
            damageUpgradeCost = Mathf.RoundToInt(damageUpgradeCost * 1.05f);
            UpdatePriceUI();
        }
    }
    public void bulletspeedUP()
    {
        if (!MoneyManager.Instance.TrySpend(bulletSpeedUpgradeCost))
        {
            SoundManagement.Instance.PlayUI(SoundManagement.Instance.DontBuyGallo);
            return;
        }
        SoundManagement.Instance.PlayUI(SoundManagement.Instance.BuyGallo);
        Projectile projectile = FindFirstObjectByType<Projectile>();
        if (projectile != null)
        {
            projectile.speed += 1;
            bulletSpeedUpgradeCost = Mathf.RoundToInt(bulletSpeedUpgradeCost * 1.05f);
            UpdatePriceUI();
        }
    }
    public void firerateUP()
    {
        if (!MoneyManager.Instance.TrySpend(firerateUpgradeCost))
        {
            SoundManagement.Instance.PlayUI(SoundManagement.Instance.DontBuyGallo);
            return;
        }
        SoundManagement.Instance.PlayUI(SoundManagement.Instance.BuyGallo);
         Tower tower = FindFirstObjectByType<Tower>();
        if (tower != null)
        {
            tower.fireCooldown *= 0.8f;
            firerateUpgradeCost = Mathf.RoundToInt(firerateUpgradeCost * 1.05f);
            UpdatePriceUI();
        }
    }
    private void UpdatePriceUI()
    {
        damagePriceText.text = "$" + damageUpgradeCost.ToString();
        bulletSpeedPriceText.text = "$" + bulletSpeedUpgradeCost.ToString();
        fireRatePriceText.text = "$" + firerateUpgradeCost.ToString();
    }

}
