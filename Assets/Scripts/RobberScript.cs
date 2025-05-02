using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum AIState {Seek, Flee, Pursue, Evade, Wander, Hide}

public class RobberScript : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;
    [SerializeField] Drive targetDrive;
    private float targetSpeed;
    private float distanceToTarget;
    private Vector3 wanderTarget = Vector3.zero;
    public AIState state;
    private List<GameObject> hidingSpots = new List<GameObject>();
    private Vector3 closestHidingSpot = Vector3.zero;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        GameObject[] tempHidingSpots = GameObject.FindGameObjectsWithTag("hide");
        foreach(GameObject hs in tempHidingSpots)
            hidingSpots.Add(hs);
    }

    private void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    private void Flee(Vector3 location)
    {
        Vector3 fleeLocation = transform.position + (-1 * location - transform.position);
        agent.SetDestination(fleeLocation);
    }

    private void Pursue(Vector3 location)
    {
        targetSpeed = targetDrive.currentSpeed;
        distanceToTarget = Magnitude(location - transform.position);
        float lookAhead = distanceToTarget / (targetSpeed * agent.speed);
        agent.SetDestination(target.transform.position + target.transform.forward * lookAhead);
    }

    private void Evade(Vector3 location)
    {
        targetSpeed = targetDrive.currentSpeed;
        distanceToTarget = Magnitude(location - transform.position);
        float lookAhead = distanceToTarget / (targetSpeed * agent.speed);
        Flee(target.transform.position + target.transform.forward * lookAhead);
    }
    
    private void Wander()
    {
        float wanderRadius = 10f;
        float wanderDistance = 20f;
        float wanderJitter = 1f;

        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;
        
        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = gameObject.transform.InverseTransformVector(targetLocal);
        
        Seek(targetWorld);
    }

    private void Hide()
    {
        float minimumDistance = Mathf.Infinity;
        
        Vector3 hideDistance;
        Vector3 hidePosition;
        foreach (GameObject hs in hidingSpots)
        {
            hideDistance = hs.transform.position - target.transform.position;
            hidePosition = hs.transform.position + hideDistance.normalized * 5;
            if (Vector3.Distance(transform.position, hideDistance) < minimumDistance)
            {
                closestHidingSpot = hidePosition;
                minimumDistance = Vector3.Distance(transform.position, hideDistance);
            }
        }
       Seek(closestHidingSpot);
    }

    private void Update()
    {
        switch (state)
        {
            case AIState.Seek:
                Seek(target.transform.position);
                break;
            
            case AIState.Flee:
                Flee(target.transform.position);
                break;
            
            case AIState.Pursue:
                Pursue(target.transform.position);
                if (targetSpeed <= 0.04f)
                    state = AIState.Seek;
                break;
            
            case AIState.Evade:
                Evade(target.transform.position);
                break;
            
            case AIState.Wander:
                Wander();
                break;
            
            case AIState.Hide:
                Hide();
                break;
        }
    }
    
    private float Magnitude(Vector3 vector)
    {
        return Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2));
    }
}
