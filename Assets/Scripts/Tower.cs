using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Targeting")]
    [SerializeField] private float range = 5f;
    [SerializeField] private LayerMask enemyLayer; // set to Enemy layer in Inspector
    [SerializeField] private float targetRefreshRate = 0.2f;

    [Header("Firing")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint; // empty child transform at barrel
    public float fireCooldown = 0.6f;

    private Transform currentTarget;
    private float fireTimer;
    private float refreshTimer;

    void Update()
    {
        refreshTimer -= Time.deltaTime;
        if (refreshTimer <= 0f)
        {
            refreshTimer = targetRefreshRate;
            currentTarget = FindNearestTarget();
        }

        if (currentTarget == null) return;

        // aim (optional)
        Vector3 dir = currentTarget.position - transform.position;
        if (dir.sqrMagnitude > 0.001f)
            transform.up = dir.normalized;

        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            fireTimer = fireCooldown;
            Shoot(currentTarget);
        }
    }

    private Transform FindNearestTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        if (hits.Length == 0) return null;

        float bestDist = float.MaxValue;
        Transform best = null;

        for (int i = 0; i < hits.Length; i++)
        {
            float d = (hits[i].transform.position - transform.position).sqrMagnitude;
            if (d < bestDist)
            {
                bestDist = d;
                best = hits[i].transform;
            }
        }
        return best;
    }

    private void Shoot(Transform target)
    {
        if (projectilePrefab == null || firePoint == null) return;

        Projectile p = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        p.Init(target);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}