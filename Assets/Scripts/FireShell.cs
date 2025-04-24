using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireShell : MonoBehaviour
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject player;
    private Vector3 aim;
    void Update()
    {
        aim = CalculateTrajectory();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (aim != Vector3.zero)
            {
                Debug.Log(this.transform.forward);
                this.transform.forward =  aim;
                Instantiate(bullet, turret.transform.position, turret.transform.rotation);
            }
        }
    }

    private Vector3 CalculateTrajectory()
    {
        Vector3 position = player.transform.position - this.transform.position;
        Vector3 velocity = player.transform.forward * player.GetComponent<Move>().speed;
        float speed = bullet.GetComponent<MoveShell>().speed;
        
        float a = Vector3.Dot(velocity, velocity) - (speed * speed);
        float b = 2 * Vector3.Dot(position, velocity);
        float c = Vector3.Dot(position, position);

        float underTheRoot = (b * b) - (a * c);

        float t; //time that intersects the bullet with the player
        if (underTheRoot > 0)
        {
            float t1 = (-b + Mathf.Sqrt(underTheRoot)) / c;
            float t2 = (-b - Mathf.Sqrt(underTheRoot)) / c;

            if (t1 < 0)
                t = t2;
            else if (t2 < 0)
                t = t1;
            else
                t = Mathf.Max(t1, t2);
            
            return t * position + velocity;
        }
        return Vector3.zero;
    }
}
