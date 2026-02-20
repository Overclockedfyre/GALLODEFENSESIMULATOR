using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] Waypoints { get; private set; }

    // segmentLength[i] = distance from Waypoints[i] to Waypoints[i+1]
    private float[] segmentLength;

    // cumulativeFromStart[i] = distance from Waypoints[0] to Waypoints[i]
    private float[] cumulativeFromStart;

    public float TotalLength { get; private set; }

    private void Awake()
    {
        Debug.Log($"[Path Awake] name={name} active={gameObject.activeInHierarchy} childCount={transform.childCount}");

        Waypoints = new Transform[transform.childCount];
        for (int i = 0; i < Waypoints.Length; i++)
            Waypoints[i] = transform.GetChild(i);

        Debug.Log($"[Path Awake] Waypoints.Length={Waypoints.Length}");

        BuildCache();
    }

    private void BuildCache()
    {
        int n = Waypoints.Length;
        TotalLength = 0f;

        if (n < 2)
        {
            segmentLength = new float[0];
            cumulativeFromStart = new float[n];
            return;
        }

        segmentLength = new float[n - 1];
        cumulativeFromStart = new float[n];
        cumulativeFromStart[0] = 0f;

        for (int i = 0; i < n - 1; i++)
        {
            float len = Vector3.Distance(Waypoints[i].position, Waypoints[i + 1].position);
            segmentLength[i] = len;
            TotalLength += len;
            cumulativeFromStart[i + 1] = cumulativeFromStart[i] + len;
        }
    }

    // Distance along the path from start (WP0) to the end for an enemy currently moving toward Waypoints[nextIndex].
    // nextIndex is the "index" in EnemyFollowPath (the next waypoint the enemy is targeting).
    public float DistanceTravelled(Vector3 enemyPos, int nextIndex)
    {
        if (Waypoints == null || Waypoints.Length < 2) return 0f;

        nextIndex = Mathf.Clamp(nextIndex, 1, Waypoints.Length - 1);

        int segStart = nextIndex - 1;
        Vector3 a = Waypoints[segStart].position;
        Vector3 b = Waypoints[nextIndex].position;

        Vector3 ab = b - a;
        float abLenSq = ab.sqrMagnitude;

        float t = 0f;
        if (abLenSq > 1e-6f)
            t = Vector3.Dot(enemyPos - a, ab) / abLenSq;

        t = Mathf.Clamp01(t);

        float alongSegment = Mathf.Sqrt(abLenSq) * t;
        return cumulativeFromStart[segStart] + alongSegment;
    }

    public float RemainingDistance(Vector3 enemyPos, int nextIndex)
    {
        float travelled = DistanceTravelled(enemyPos, nextIndex);
        return Mathf.Max(0f, TotalLength - travelled);
    }

    // World position at distanceFromStart meters along the waypoint polyline (0..TotalLength).
    public Vector3 GetPositionAtDistance(float distanceFromStart)
    {
        if (Waypoints == null || Waypoints.Length == 0) return Vector3.zero;
        if (Waypoints.Length == 1) return Waypoints[0].position;

        distanceFromStart = Mathf.Clamp(distanceFromStart, 0f, TotalLength);

        // Find segment (linear scan; fine for typical TD waypoint counts)
        int seg = 0;
        while (seg < segmentLength.Length - 1 && cumulativeFromStart[seg + 1] < distanceFromStart)
            seg++;

        float segStartDist = cumulativeFromStart[seg];
        float segLen = segmentLength[seg];

        float t = segLen > 1e-6f ? (distanceFromStart - segStartDist) / segLen : 0f;

        Vector3 a = Waypoints[seg].position;
        Vector3 b = Waypoints[seg + 1].position;
        return Vector3.Lerp(a, b, t);
    }
}
