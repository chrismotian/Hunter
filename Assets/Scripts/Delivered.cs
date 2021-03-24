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
        if (this.transform.parent.TryGetComponent<PatrolWaypoints>(out route) && this.gameObject.tag != "Enemy")
        {
            route.WPoints[0] = new Vector2(1f, -5f);
            route.WPoints[1] = new Vector2(1f, -1f);
            route.WPoints[2] = new Vector2(Customer.transform.position.x, Customer.transform.position.y);
            route.WPoints[3] = new Vector2(0f, 3f);
            route.WPoints[4] = new Vector2(2f, 3f);
            route.WPoints[5] = new Vector2(2f, -4f);
        }
        if (customerCollider != null && customerCollider.OverlapPoint(this.transform.position))
        {
            this.GetComponentInParent<TakeByCollision>().numberOfTakeAways = 0;
            Destroy(TakeAwayOrder);
            route.WPoints[0] = new Vector2(1f, -5f);
            route.WPoints[1] = new Vector2(1f, 3f);
            route.WPoints[2] = new Vector2(-1f, 4f);
            route.WPoints[3] = new Vector2(0.5f, 5f);
            route.WPoints[4] = new Vector2(2f, 4f);
            route.WPoints[5] = new Vector2(2f, -4f);
            Destroy(this.gameObject);
        }
    }
}
