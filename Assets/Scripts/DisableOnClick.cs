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
        if (Input.touchCount >= 1)
        {
            touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            if (bodyCollider.OverlapPoint(touchPosition))
            {
                this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (bodyCollider.OverlapPoint(mousePosition))
            {
                this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
            }
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            if (this.GetComponent<Rigidbody2D>().velocity.y < -1)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.y + 0.1f, this.GetComponent<Rigidbody2D>().velocity.x);
            }
            else if (this.GetComponent<Rigidbody2D>().velocity.y > 1)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.y - 0.1f, this.GetComponent<Rigidbody2D>().velocity.x);
            }
            else if (this.GetComponent<Rigidbody2D>().velocity.x > 1)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.y, this.GetComponent<Rigidbody2D>().velocity.x - 0.1f);
            }
            else if (this.GetComponent<Rigidbody2D>().velocity.x < -1)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.y, this.GetComponent<Rigidbody2D>().velocity.x + 0.1f);
            }
            else
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }
}
