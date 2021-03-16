using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskInteractionByZoom : MonoBehaviour
{
    [SerializeField] ZoomByControl zoomChangeEvent = null;
    private void Start()
    {
        zoomChangeEvent.OnZoomChange += ZoomByControl_OnZoomChange;
    }
    private void ZoomByControl_OnZoomChange(object sender, ZoomByControl.OnZoomChangeEventArgs e)
    {
        if (e.In)
        {
            this.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        }
    }
    private void OnDestroy()
    {
        zoomChangeEvent.OnZoomChange -= ZoomByControl_OnZoomChange;
    }
}
