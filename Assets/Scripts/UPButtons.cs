using UnityEngine;
using UnityEngine.SceneManagement;


public class UPButtons : MonoBehaviour
{

    public void damageUP()
    {
        Projectile projectile = FindFirstObjectByType<Projectile>();
        if (projectile != null)
        {
            projectile.damage += 1;
        }
    }
    public void bulletspeedUP()
    {
        Projectile projectile = FindFirstObjectByType<Projectile>();
        if (projectile != null)
        {
            projectile.speed += 1;
        }
    }
    public void firerateUP()
    {
         Tower tower = FindFirstObjectByType<Tower>();
        if (tower != null)
        {
            tower.fireCooldown *= 0.8f;
        }
    }

}
