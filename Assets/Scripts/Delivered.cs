using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivered : MonoBehaviour
{
    Vector2 originalPosition = Vector2.zero;
    int indexAtTake = 0;
    GameObject TakeAwayOrder;
    GameObject Customer;
    [SerializeField] Collider2D customerCollider = null;
    // Start is called before the first frame update
    void Start()
    {
        TakeAwayOrder = this.transform.parent.gameObject;
        Customer = customerCollider.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        MoveWithPulseAndSpawn route = null;
        if (this.transform.parent.TryGetComponent<MoveWithPulseAndSpawn>(out route))
        {
            if (this.transform.parent.gameObject.tag == "Friend")
            {
                indexAtTake = route.currentIndex;
                originalPosition = route.waypoints[indexAtTake + 1];
                route.waypoints[indexAtTake + 1] = Customer.transform.position;
            }else if(this.transform.parent.gameObject.tag == "Enemy"){
                for (int i = 0; i < route.waypoints.Count; i++)
                {
                    route.waypoints[i] = new Vector2(-5, 0);
                }
            }
        }
        if (customerCollider != null && customerCollider.OverlapPoint(this.transform.position))
        {
            this.GetComponentInParent<TakeByCollision>().numberOfTakeAways = 0;
            Destroy(TakeAwayOrder);
            route.waypoints[indexAtTake + 1] = originalPosition;
            Destroy(this.gameObject);
        }
    }
}
