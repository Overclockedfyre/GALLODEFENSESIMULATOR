using UnityEngine;

public class EnemyFollowPath : MonoBehaviour
{
    [SerializeField] private Path path;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float reachDistance = 0.05f;

    [Header("Rotation")]
    [SerializeField] private bool rotateToMoveDir = true;
    [SerializeField] private float turnSpeedDegPerSec = 720f;

    private int index;

    public void Init(Path p)
    {
        path = p;
        index = 0;

        if (path == null || path.Waypoints == null || path.Waypoints.Length == 0)
        {
            Debug.LogError("EnemyFollowPath: Path missing/empty.");
            enabled = false;
            return;
        }

        // Optional: spawn at first waypoint
        transform.position = path.Waypoints[0].position;
        index = 1;
    }

    private void Update()
    {
        if (path == null || index >= path.Waypoints.Length)
        {
            OnReachedEnd();
            return;
        }

        Vector3 target = path.Waypoints[index].position;
        Vector3 toTarget = target - transform.position;

        float step = speed * Time.deltaTime;

        // Overshoot-safe movement
        Vector3 move = Vector3.ClampMagnitude(toTarget, step);
        transform.position += move;

        // Rotation toward movement direction
        if (rotateToMoveDir && move.sqrMagnitude > 1e-6f)
        {
            Quaternion desired = Quaternion.LookRotation(move.normalized, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, desired, turnSpeedDegPerSec * Time.deltaTime
            );
        }

        if (toTarget.sqrMagnitude <= reachDistance * reachDistance)
            index++;
    }

    private void OnReachedEnd()
    {
        // TODO: damage base, award money, etc.
        Destroy(gameObject);
    }
}
