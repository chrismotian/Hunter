using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    [SerializeField] Scope scope;
    void Update()
    {
        if (this.transform.position.y < -5)
        {
            scope.enemyList.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
