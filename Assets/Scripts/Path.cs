using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] Waypoints { get; private set; }

    private void Awake()
    {
        Waypoints = new Transform[transform.childCount];
        for (int i = 0; i < Waypoints.Length; i++)
            Waypoints[i] = transform.GetChild(i);
    }
}