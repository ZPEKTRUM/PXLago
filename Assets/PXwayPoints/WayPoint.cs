using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{

    [Header("Waypoint Status")]

    public WayPoint previousWaypoint;
    public WayPoint nextWaypoint;

    [Range(0f, 5f)]
    public float WaypointWidth = 1f;
    public List<WayPoint> branches = new List<WayPoint>();

    [Range(0f, 1f)]
    public float branchesRatio = 0.5f;
    internal float branchRatio;

    // internal Vector3 waypointWidth;

    public Vector3 GetPosition()

    {
        Vector3 minBound = transform.position + transform.right * WaypointWidth / 3f;
        Vector3 maxBound = transform.position + transform.right * WaypointWidth / 2f;

        return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));  //Random Lerp

    }

}
