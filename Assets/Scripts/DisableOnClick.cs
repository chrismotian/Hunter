using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnClick : MonoBehaviour
{
    Vector3 touchPosition = Vector3.zero;
    Collider2D bodyCollider = null;
    private void Start()
    {
        bodyCollider = this.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (bodyCollider.OverlapPoint(mousePosition))
            {
                this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                Destroy(this);
            }
        }else if(Input.touchCount >= 1 && Input.touches[0].phase == TouchPhase.Ended)
        { 
            if (bodyCollider.OverlapPoint(touchPosition))
            {
                this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                Destroy(this);
            }
        }
    }
}
