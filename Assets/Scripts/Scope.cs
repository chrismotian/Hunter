using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    [SerializeField] ZoomByControl zoomChangeEvent = null;
    bool scopeEnabled = false;
    Vector3 mousePos = Vector3.zero;
    Vector3 touchPosition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        zoomChangeEvent.OnZoomChange += ZoomByControl_OnZoomChange;
    }

    // Update is called once per frame
    void Update()
    {
        if (scopeEnabled)
        {
            if (Input.touchCount >= 1)
            {
                touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                transform.position = new Vector3(touchPosition.x, touchPosition.y, 0f);
            }
            else if (Input.GetMouseButton(0))
            {

                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
            }
        }
    }
    private void ZoomByControl_OnZoomChange(object sender, ZoomByControl.OnZoomChangeEventArgs e)
    {
        if (e.In)
        {
            scopeEnabled = true;
        }
        else
        {
            scopeEnabled = false;
        }
    }
    private void OnDestroy()
    {
        zoomChangeEvent.OnZoomChange -= ZoomByControl_OnZoomChange;
    }
}