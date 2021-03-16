using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    void Update()
    {
        if (this.transform.position.y < -3)
        {
            Destroy(this.gameObject);
        }
    }
}
