using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ZoomByControl : MonoBehaviour
{
    float zoomInFactor = 0; 
    [SerializeField] float zoomFactor = 60f;
    private float targetPPU = 128f;
    [SerializeField] CameraFollow cameraFollow = null;
    ScrollByControl scrollByControl = null;
    [SerializeField] Scope scope = null;


    void Start()
    {
        targetPPU = this.gameObject.GetComponent<PixelPerfectCamera>().assetsPPU;
        zoomInFactor = zoomFactor;
        scrollByControl = this.gameObject.GetComponent<ScrollByControl>();
    }
    void Update()
    {
        //Debug.Log(targetPPU);
        if ((Input.GetMouseButton(0) || Input.touchCount >= 1))
        {
            //Zoom In slower when close to the final value
            zoomInFactor = zoomInFactor / 2 + zoomFactor * (1f - ((float)this.gameObject.GetComponent<PixelPerfectCamera>().assetsPPU / (float)256));
            targetPPU += zoomInFactor * Time.deltaTime;
            scrollByControl.scrollingEnabled = true;
        }
        else
        {
            //Zoom out always with the same factor
            targetPPU -= zoomFactor * Time.deltaTime;
            cameraFollow.Setup(() => Vector3.zero);
            scrollByControl.scrollingEnabled = false;
        }
        targetPPU = Mathf.Clamp(targetPPU, 128, 256);
        this.gameObject.GetComponent<PixelPerfectCamera>().assetsPPU = (int)targetPPU;
    }
}
