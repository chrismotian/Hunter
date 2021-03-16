using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using System;

public class ZoomByControl : MonoBehaviour
{
    public event EventHandler<OnZoomChangeEventArgs> OnZoomChange;
    public class OnZoomChangeEventArgs : EventArgs
    {
        public bool In;
    }

    float zoomInFactor = 0; 
    [SerializeField] float zoomFactor = 60f;
    private float targetPPU = 128f;
    [SerializeField] CameraFollow cameraFollow = null;

    void Start()
    {
        targetPPU = this.gameObject.GetComponent<PixelPerfectCamera>().assetsPPU;
        zoomInFactor = zoomFactor;
    }
    void Update()
    {
        //Debug.Log(targetPPU);
        if ((Input.GetMouseButton(0) || Input.touchCount >= 1))
        {
            //Zoom In slower when close to the final value
            zoomInFactor = zoomInFactor / 2 + zoomFactor * (1f - ((float)this.gameObject.GetComponent<PixelPerfectCamera>().assetsPPU / (float)256));
            targetPPU += zoomInFactor * Time.deltaTime;
            OnZoomChange?.Invoke(this, new OnZoomChangeEventArgs
            {
                In = true
            }); ;
        }
        else
        {
            //Zoom out always with the same factor
            targetPPU -= zoomFactor * Time.deltaTime;
            cameraFollow.Setup(() => Vector3.zero);
            OnZoomChange?.Invoke(this, new OnZoomChangeEventArgs
            {
                In = false
            }); ;
        }
        targetPPU = Mathf.Clamp(targetPPU, 128, 256);
        this.gameObject.GetComponent<PixelPerfectCamera>().assetsPPU = (int)targetPPU;
    }


}
