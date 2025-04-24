using UnityEngine;

public class Late : MonoBehaviour
{
    void LateUpdate()
    {
        this.transform.Translate(0, 0, 0.1f);
    }
}
