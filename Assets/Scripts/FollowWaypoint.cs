using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoint : MonoBehaviour
{
    [SerializeField] private List<GameObject> waypoints = new List<GameObject>();
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private int accuracy;
    private GameObject currentWaypoint;
    private int currentWaypointIndex = 0;
    
    void Start()
    {
        currentWaypoint = waypoints[0];
    }

    void Update()
    {
        if (Vector3.Distance(currentWaypoint.transform.position, this.transform.position) < accuracy)
        {
            currentWaypointIndex++;
            currentWaypoint = waypoints[currentWaypointIndex];
        }

        if (currentWaypointIndex >= waypoints.Count - 1)
            currentWaypointIndex = 0;
        
        Quaternion lookRotation = Quaternion.LookRotation(currentWaypoint.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
