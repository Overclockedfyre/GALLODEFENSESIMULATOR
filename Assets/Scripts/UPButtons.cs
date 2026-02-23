using UnityEngine;
using UnityEngine.SceneManagement;


public class UPButtons : MonoBehaviour
{
    private int damageUpgradeCost = 50;
    private int bulletSpeedUpgradeCost = 50;
    private int firerateUpgradeCost = 50;
    public void damageUP()
    {
        if (!MoneyManager.Instance.TrySpend(damageUpgradeCost))
        {
            return;
        }

        Projectile projectile = FindFirstObjectByType<Projectile>();
        if (projectile != null)
        {
            projectile.damage += 1;
            damageUpgradeCost += 5;
        }
    }
    public void bulletspeedUP()
    {
        if (!MoneyManager.Instance.TrySpend(bulletSpeedUpgradeCost))
        {
            return;
        }

        Projectile projectile = FindFirstObjectByType<Projectile>();
        if (projectile != null)
        {
            projectile.speed += 1;
            bulletSpeedUpgradeCost += 5;
        }
    }
    public void firerateUP()
    {
        if (!MoneyManager.Instance.TrySpend(firerateUpgradeCost))
        {
            return;
        }

         Tower tower = FindFirstObjectByType<Tower>();
        if (tower != null)
        {
            tower.fireCooldown *= 0.8f;
            firerateUpgradeCost += 5;
        }
    }

}
