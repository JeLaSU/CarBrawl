using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DrawPathHandler : MonoBehaviour
{
    public Transform transformRootObject;

    WaypointNode[] waypointNodes;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (transformRootObject == null)
            return;

        //Get all Waypoints
        waypointNodes = transformRootObject.GetComponentsInChildren<WaypointNode>();

        //Iterate the list
        foreach (WaypointNode waypoint in waypointNodes)
        {
       
            foreach (WaypointNode nextWayPoint in waypoint.nextWaypointNode)
            {
                if (nextWayPoint != null)
                    Gizmos.DrawLine(waypoint.transform.position, nextWayPoint.transform.position);

            }

        }
    }

}
