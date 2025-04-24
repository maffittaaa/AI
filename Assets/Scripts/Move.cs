using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Move : MonoBehaviour
{
    //[SerializeField] private GameObject goal;
    public float speed;
    private Vector3 directionToGoalVector;
    private Vector3 facingDirectionVector;
    private float threshold = 2f;
    private float dotProduct;
    private float angleBetweenThem;

    void Update()
    {
        //directionToGoalVector = goal.transform.position - this.transform.position;
        //facingDirectionVector = this.transform.forward;

        //if (Input.GetKeyDown(KeyCode.W))
            //AngleBetween();

        //if (directionToGoalVector.magnitude > threshold)
            //this.transform.position += directionToGoalVector.normalized * speed;
            transform.Translate(Time.deltaTime * speed, 0, 0);
    }

    private float DotProduct(Vector3 vector1, Vector3 vector2)
    {
        return vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z;
    }

    private float Magnitude(Vector3 vector)
    {
        return Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2));
    }

    private Vector3 CrossProduct(Vector3 vector1, Vector3 vector2)
    {
        return new Vector3(vector1.y * vector2.z - vector1.z * vector2.y, vector1.z * vector2.x - vector1.x * vector2.z, vector1.x * vector2.y - vector1.y * vector2.x);
    }

    private float AngleBetween()
    {
        angleBetweenThem = Mathf.Acos(DotProduct(directionToGoalVector, facingDirectionVector) /
                                      (Magnitude(directionToGoalVector) * Magnitude(facingDirectionVector))); // Dot product - 0 is perpendicle;
        angleBetweenThem = Mathf.Rad2Deg * angleBetweenThem;
        
        Vector3 crossProduct = CrossProduct(directionToGoalVector, facingDirectionVector); //direction where player is facing that is why we do cross product

        int clockwise = 1;
        if (crossProduct.y > 0) // negative - clockwise; positive - anticlockwise
            clockwise *= -1;
        
        if (angleBetweenThem > 10)
            this.transform.Rotate(0, angleBetweenThem * clockwise, 0);
        
        Debug.Log("myAngle: " + angleBetweenThem);
        Debug.Log("Unity: " + Vector3.Angle(facingDirectionVector, directionToGoalVector));

        return angleBetweenThem;
    }
}
