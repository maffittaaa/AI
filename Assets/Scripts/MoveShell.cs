using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShell : MonoBehaviour
{
    public float speed;
    
    void Update()
    {
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
