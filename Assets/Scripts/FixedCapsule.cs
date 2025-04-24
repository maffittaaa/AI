using UnityEngine;

public class FixedCapsule : MonoBehaviour
{
    void FixedUpdate()
    {
        this.transform.Translate(0, 0, 0.1f);
    }
}
