using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] path;
    public float speed = 3f;

    private int targetIndex = 0;

    void Update()
    {
        if (path == null || path.Length == 0)
            return;

        if (targetIndex >= path.Length)
            return;

        Transform target = path[targetIndex];

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            targetIndex++;
        }
    }
}
