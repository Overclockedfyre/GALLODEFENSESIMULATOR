using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 12f;
    public float damage = 2f;
    [SerializeField] private float lifetime = 3f;

    private Transform target;

    public void Init(Transform targetTransform)
    {
        target = targetTransform;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = (target.position - transform.position);
        float distThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distThisFrame)
        {
            HitTarget();
            return;
        }

        transform.position += dir.normalized * distThisFrame;
        transform.up = dir.normalized; // optional: rotate to face target
    }

    private void HitTarget()
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}