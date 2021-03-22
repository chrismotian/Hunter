using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    [SerializeField] ZoomByControl zoomChangeEvent = null;
    bool scopeEnabled = false;
    Vector3 mousePos = Vector3.zero;
    Vector3 touchPosition = Vector3.zero;
    Vector3 mousePosition = Vector3.zero;
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
                transform.position = new Vector3(touchPosition.x, touchPosition.y, 0);
            }
            else if (SystemInfo.deviceType != DeviceType.Handheld)
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
            }
            else
            {
                transform.position = Vector2.zero;
            }
        }
    }
    private void ZoomByControl_OnZoomChange(object sender, ZoomByControl.OnZoomChangeEventArgs e)
    {
        if (e.In)
        {
            scopeEnabled = true;
            this.transform.localScale = new Vector3(1, 1, 1);
            this.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            scopeEnabled = false;
            this.transform.localScale = new Vector3(2, 2, 2);
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    private void OnDestroy()
    {
        zoomChangeEvent.OnZoomChange -= ZoomByControl_OnZoomChange;
    }
}