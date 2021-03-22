using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundByZoomOut : MonoBehaviour
{

    [SerializeField] ZoomByControl zoomChangeEvent = null;

    void Start()
    {
        zoomChangeEvent.OnZoomChange += ZoomByControl_OnZoomChange;
    }

    private void ZoomByControl_OnZoomChange(object sender, ZoomByControl.OnZoomChangeEventArgs e)
    {
        if (e.In)
        {
            this.GetComponent<AudioSource>().Play();
        }
        else
        {
        }
    }
}
