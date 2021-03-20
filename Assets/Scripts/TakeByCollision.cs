using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeByCollision : MonoBehaviour
{
    public int numberOfTakeAways = 0;

    void OnTriggerEnter2D(Collider2D takeaway)
    {
        if (takeaway.tag=="TakeAway" && numberOfTakeAways == 0)
        {
            numberOfTakeAways = 1;
            Destroy(takeaway.gameObject.GetComponent<Rigidbody2D>());
            takeaway.transform.parent = this.transform;
        }
    }
}
