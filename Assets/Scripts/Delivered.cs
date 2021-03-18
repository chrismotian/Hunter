using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivered : MonoBehaviour
{
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
        PatrolWaypoints route = null;
        if (this.transform.parent.TryGetComponent<PatrolWaypoints>(out route))
        {
            route.WPoints[0] = new Vector2(-3.5f, 3);
            route.WPoints[1] = new Vector2(1.5f, -0.5f);
            route.WPoints[2] = new Vector2(1.5f, -2.2f);
            route.WPoints[3] = new Vector2(1.5f, -0.5f);
            route.WPoints[4] = new Vector2(-3.5f, 3);
            route.WPoints[5] = new Vector2(Customer.transform.position.x, Customer.transform.position.y);
        }
        if (customerCollider.OverlapPoint(this.transform.position))
        {
            this.GetComponentInParent<TakeByCollision>().numberOfTakeAways = 0;
            Destroy(TakeAwayOrder);
            route.WPoints[0] = new Vector2(-3.5f, 3.2f);
            route.WPoints[1] = new Vector2(1.5f, -0.5f);
            route.WPoints[2] = new Vector2(1.5f, -2.2f);
            route.WPoints[3] = new Vector2(3.37f, 0.56f);
            route.WPoints[4] = new Vector2(4.11f, 1.96f);
            route.WPoints[5] = new Vector2(-3.5f, 3.2f);
            Destroy(this.gameObject);
        }
    }
}
