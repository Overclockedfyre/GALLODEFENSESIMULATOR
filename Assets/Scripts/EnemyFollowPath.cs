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

    public float DistanceToExit { get; private set; }
    public float DistanceFromStart { get; private set; }
    public Vector3 CurrentPosition => transform.position;

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

        transform.position = path.Waypoints[0].position;
        index = 1;

        DistanceFromStart = path.DistanceTravelled(transform.position, index);
        DistanceToExit = path.RemainingDistance(transform.position, index);
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
        Vector3 move = Vector3.ClampMagnitude(toTarget, step);
        transform.position += move;

        if (rotateToMoveDir && move.sqrMagnitude > 1e-6f)
        {
            Quaternion desired = Quaternion.LookRotation(move.normalized, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, desired, turnSpeedDegPerSec * Time.deltaTime
            );
        }

        DistanceFromStart = path.DistanceTravelled(transform.position, index);
        DistanceToExit = path.RemainingDistance(transform.position, index);

        if (toTarget.sqrMagnitude <= reachDistance * reachDistance)
            index++;
    }

    private void OnReachedEnd()
    {
        Destroy(gameObject);
    }
}