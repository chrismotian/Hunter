using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnClick : MonoBehaviour
{
    Collider2D bodyCollider = null;
    private void Start()
    {
        bodyCollider = this.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (bodyCollider.OverlapPoint(mousePosition))
            {
                this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                Destroy(this);
            }
        }
        else if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
                if (bodyCollider.OverlapPoint(touchPosition))
                {
                    this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                    Destroy(this);
                }
            }
        }
    }
}
