using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    private Vector3 mousePos;
    Vector3 touchPosition = Vector3.zero;
    [SerializeField] SpriteRenderer clearBackground = null;
    [SerializeField] public List<GameObject> enemyList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >= 1)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }
            clearBackground.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            transform.position = new Vector3(touchPosition.x, touchPosition.y, 0f);
        }
        else if (Input.GetMouseButton(0))
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }
            clearBackground.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x,mousePosition.y,0);
            Debug.Log(transform.position + " and my layer is" + this.GetComponent<SpriteRenderer>().sortingOrder);
        }
        else
        {
            clearBackground.maskInteraction = SpriteMaskInteraction.None;
            for (int i = 0; i <  enemyList.Count; i++) {
                enemyList[i].GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            }
        }
    }
}