using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class ScrollByControl : MonoBehaviour
{
    [SerializeField] ZoomByControl zoomChangeEvent = null;
    bool scrollingEnabled = false;
    [SerializeField] float moveAmount = 10f;
    [SerializeField] float edgeSize = 50f;
    public CameraFollow cameraFollow;
    float maxScrollingValueX = 5;
    float maxScrollingValueY = 4;
    Vector3 cameraFollowPosition = Vector3.zero;
    Vector3 touchPosition = Vector3.zero;

    void Start()
    {
        maxScrollingValueX = 4.21875f / 6;
        maxScrollingValueY = 4.21875f / 1.8f;
        cameraFollow.Setup(() => cameraFollowPosition);

        zoomChangeEvent.OnZoomChange += ZoomByControl_OnZoomChange;
    }

    void Update()
    {
        if (Input.touchCount >= 1)
        {
            touchPosition = Input.touches[0].position;
        }
        if (scrollingEnabled)
        {
            if ((Input.mousePosition.x > Screen.width - edgeSize || touchPosition.x > Screen.width - (edgeSize * 2)) && cameraFollow.transform.position.x <  maxScrollingValueX)
            {
                cameraFollowPosition.x += moveAmount * Time.deltaTime;
                cameraFollow.Setup(() => cameraFollowPosition);
            }
            else if ((Input.mousePosition.x < edgeSize || touchPosition.x < (edgeSize * 2)) && cameraFollow.transform.position.x > -maxScrollingValueX)
            {
                cameraFollowPosition.x -= moveAmount * Time.deltaTime;
                cameraFollow.Setup(() => cameraFollowPosition);
            }
            else if ((Input.mousePosition.y > Screen.height - (edgeSize * 5) || touchPosition.y > Screen.height - (edgeSize * 20)) && cameraFollow.transform.position.y < maxScrollingValueY)
            {
                cameraFollowPosition.y += moveAmount * Time.deltaTime;
                cameraFollow.Setup(() => cameraFollowPosition);
            }
            else if ((Input.mousePosition.y < (edgeSize * 5) || touchPosition.y < (edgeSize * 20)) && cameraFollow.transform.position.y > -maxScrollingValueY)
            {
                cameraFollowPosition.y -= moveAmount * Time.deltaTime;
                cameraFollow.Setup(() => cameraFollowPosition);
            }
        }
        else
        {
            if (cameraFollow.transform.position.x < - 30)
            {
                cameraFollowPosition.x += moveAmount * Time.deltaTime;
            }
            else if (cameraFollow.transform.position.x > 30)
            {
                cameraFollowPosition.x -= moveAmount * Time.deltaTime;
            }
            else if (cameraFollow.transform.position.y < - 30)
            {
                cameraFollowPosition.y += moveAmount * Time.deltaTime;
            }
            else if (cameraFollow.transform.position.y > 30)
            {
                cameraFollowPosition.y -= moveAmount * Time.deltaTime;
            }
            else
            {
                cameraFollowPosition = Vector3.zero;
            }
            cameraFollow.Setup(() => cameraFollowPosition);
        }
    }

    private void ZoomByControl_OnZoomChange(object sender, ZoomByControl.OnZoomChangeEventArgs e)
    {
        if (e.In)
        {
            scrollingEnabled = true;
        }
        else
        {
            scrollingEnabled = false;
        }
    }
    private void OnDestroy()
    {
        zoomChangeEvent.OnZoomChange -= ZoomByControl_OnZoomChange;
    }
}
