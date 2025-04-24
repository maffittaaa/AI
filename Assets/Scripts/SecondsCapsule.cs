using UnityEngine;

public class SecondsCapsule : MonoBehaviour
{
    //keep track of when our update starts so that we can take away any sort of thicks that happened before
    private float timeStartOffset = 0;

    void Start()
    {
        timeStartOffset = Time.realtimeSinceStartup;
        Debug.Log(this.transform.position);
    }
    
    void Update()
    {
        this.transform.Translate(0, 0, Time.realtimeSinceStartup - timeStartOffset);
    }
}
