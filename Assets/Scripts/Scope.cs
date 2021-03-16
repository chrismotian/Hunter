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

        if (Input.touchCount >= 1)
        {
            touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            mousePosition = Camera.main.ScreenToWorldPoint(touchPosition);
        }
        else if (SystemInfo.deviceType != DeviceType.Handheld) { 
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
            
        
    }
    private void ZoomByControl_OnZoomChange(object sender, ZoomByControl.OnZoomChangeEventArgs e)
    {
        if (e.In)
        {
            scopeEnabled = true;
            this.GetComponent<SpriteRenderer>().sortingOrder = 2;
            this.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            scopeEnabled = false;
            this.GetComponent<SpriteRenderer>().sortingOrder = 5;
            this.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
            this.transform.localScale = new Vector3(2, 2, 2);
        }
    }
    private void OnDestroy()
    {
        zoomChangeEvent.OnZoomChange -= ZoomByControl_OnZoomChange;
    }
}