using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class ScrollByControl : MonoBehaviour
{
    public bool scrollingEnabled = false;
    [SerializeField] float moveAmount = 10f;
    [SerializeField] float edgeSize = 500f;
    public CameraFollow cameraFollow;
    float maxScrollingValue = 5;
    Vector3 cameraFollowPosition = Vector3.zero;
    Vector3 touchPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        maxScrollingValue = 4.21875f / 2;
        cameraFollow.Setup(() => cameraFollowPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount >= 1)
        {
            touchPosition = Input.touches[0].position;
        }
        if (scrollingEnabled)
        {
            if ((Input.mousePosition.x > Screen.width - edgeSize || touchPosition.x > Screen.width - edgeSize) && cameraFollow.transform.position.x < maxScrollingValue)
            {
                cameraFollowPosition.x += moveAmount * Time.deltaTime;
            }
            else if ((Input.mousePosition.x < edgeSize || touchPosition.x > Screen.width - edgeSize) && cameraFollow.transform.position.x > -maxScrollingValue)
            {
                cameraFollowPosition.x -= moveAmount * Time.deltaTime;
            }
            else if ((Input.mousePosition.y > Screen.height - edgeSize/2 || touchPosition.x > Screen.width - edgeSize) && cameraFollow.transform.position.y < maxScrollingValue)
            {
                cameraFollowPosition.y += moveAmount * Time.deltaTime;
            }
            else if ((Input.mousePosition.y < edgeSize/2 || touchPosition.x > Screen.width - edgeSize) && cameraFollow.transform.position.y > -maxScrollingValue)
            {
                cameraFollowPosition.y -= moveAmount * Time.deltaTime;
            }
            cameraFollow.Setup(() => cameraFollowPosition);
        }
    }
}
