using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using UnityEngine.tvOS;

public class WayPointManagerWindow : EditorWindow
{
    [MenuItem("Waypoint/Waypoints Editor Tools")]

    public static void ShowWindow()

    {

        GetWindow<WayPointManagerWindow>("WaypointsEditor Tools");


    }

    public Transform waypointOrigin;

    private void OnGUI()  //Fonction

    {

        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointOrigin"));

        if (waypointOrigin == null)

        {

            EditorGUILayout.HelpBox("Please assign a WayPoint origin transform.", MessageType.Warning);

        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            CreateButtons();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();

    }

    void CreateButtons()
    {
        if (GUILayout.Button("Create Waypoint"))

        {

            CreateWaypoint();

        }

        if (Selection.activeGameObject != null && Selection.activeObject.GetComponent<WayPoint>())

        {
            if (GUILayout.Button("Create Waypoint Before"))
            {
                CreateWayPointBefore();
            }
            if (GUILayout.Button("Create WayPoint After"))
            {

                CreateWaypointAfter();
            }
            if (GUILayout.Button("Add Branch Waypoint"))
            {
                CreateBranch();
            }
            if (GUILayout.Button("Remove WayPoint"))
            {
                RemoveWayPoint();
            }
        }
    }

    void CreateWaypoint()
    {

        GameObject waypointObject = new GameObject("Waypoint" + waypointOrigin.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(waypointOrigin, false);

        WayPoint waypoint = waypointObject.GetComponent<WayPoint>();
        if (waypointOrigin.childCount > 1)

        {
            waypoint.previousWaypoint = waypointOrigin.GetChild(waypointOrigin.childCount - 2).GetComponent<WayPoint>();
            waypoint.previousWaypoint.nextWaypoint = waypoint;

            waypoint.transform.position = waypoint.previousWaypoint.transform.position;
            waypoint.transform.forward = waypoint.previousWaypoint.transform.forward;
        }

        Selection.activeObject = waypoint.gameObject;

    }

    void CreateWayPointBefore()

    {

        GameObject waypointObject = new GameObject("Waypoint" + waypointOrigin.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(waypointOrigin, false);

        WayPoint newWaypoint = waypointObject.GetComponent<WayPoint>();

        WayPoint selectedWaypoint = Selection.activeObject.GetComponent<WayPoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.position = selectedWaypoint.transform.forward;

        if (selectedWaypoint.previousWaypoint)

        {

            newWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
            selectedWaypoint.previousWaypoint.nextWaypoint = newWaypoint;
        }

        newWaypoint.nextWaypoint = selectedWaypoint;
        selectedWaypoint.previousWaypoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeObject = newWaypoint.gameObject;
    }

    void CreateWaypointAfter()
    {
        GameObject waypointObject = new GameObject("Waypoint" + waypointOrigin.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(waypointOrigin, false);

        WayPoint newWaypoint = waypointObject.GetComponent<WayPoint>();

        WayPoint selectedWaypoint = Selection.activeObject.GetComponent<WayPoint>();

        waypointObject.transform.position = selectedWaypoint.transform.position;
        waypointObject.transform.position = selectedWaypoint.transform.forward;

        if (selectedWaypoint.nextWaypoint != null)

        {

            selectedWaypoint.nextWaypoint.previousWaypoint = newWaypoint;
            newWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
        }

        selectedWaypoint.nextWaypoint = newWaypoint;

        newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
        Selection.activeObject = newWaypoint.gameObject;
    }
    void CreateBranch() 
    
    {

        GameObject waypointObject = new GameObject("Waypoint" + waypointOrigin.childCount, typeof(WayPoint));
        waypointObject.transform.SetParent(waypointOrigin, false);

        WayPoint WayPoint = waypointObject.GetComponent<WayPoint>();

        WayPoint branchFrom = Selection.activeGameObject.GetComponent<WayPoint>(); 
        branchFrom.branches.Add(WayPoint);

        WayPoint.transform.position = branchFrom.transform.position;
        WayPoint.transform.forward = branchFrom.transform.forward;

        Selection.activeGameObject = WayPoint.gameObject;


    }

    void RemoveWayPoint()

    {

        WayPoint selectedWaypoint = Selection.activeObject.GetComponent<WayPoint>();

        if (selectedWaypoint.nextWaypoint != null)

        {

            selectedWaypoint.nextWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;

        }

        if (selectedWaypoint.previousWaypoint != null)

        {
            selectedWaypoint.previousWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
            Selection.activeGameObject = selectedWaypoint.previousWaypoint.gameObject; 

            Destroy(selectedWaypoint.nextWaypoint.gameObject);
        }




    }
}
