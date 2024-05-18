using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class WayPointEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawGizmos(WayPoint waypoint, GizmoType gizmoType) 
    { 
     if((gizmoType & GizmoType.Selected) != 0) 
        
        { 
        
            Gizmos.color = Color.blue;
        
        }
     else 
        {
            Gizmos.color = Color.blue * 0.5f; 
        }

        Gizmos.DrawSphere(waypoint.transform.position, 0.1f);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(waypoint.transform.position +
            (waypoint.transform.right * waypoint.WaypointWidth / 2f),
            waypoint.transform.position - (waypoint.transform.right * waypoint.WaypointWidth / 2f));

        // new draw a line from preview to next way point

        if(waypoint.previousWaypoint != null) 
        
        {

            Gizmos.color = Color.red;
            Vector3 offset = waypoint.transform.right * waypoint.WaypointWidth / 3f;
            Vector3 offsetTo =  waypoint.transform.right * waypoint.previousWaypoint.WaypointWidth / 3f;

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.previousWaypoint.transform.position + offsetTo);

        
        }


        if (waypoint.nextWaypoint != null) //is not equal

        {

            Gizmos.color = Color.green;
            Vector3 offset = waypoint.transform.right * - waypoint.WaypointWidth / 3f;
            Vector3 offsetTo = waypoint.transform.right * - waypoint.previousWaypoint.WaypointWidth / 3f;

            Gizmos.DrawLine(waypoint.transform.position + offset, waypoint.previousWaypoint.transform.position + offsetTo);


        }

        if(waypoint.branches != null)
        
            foreach(WayPoint branch in waypoint.branches)   
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(waypoint.transform.position, branch.transform.position);    
        }
    }
}



