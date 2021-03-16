using UnityEngine;
using System.Collections.Generic;
using PolyNav;

//example
[RequireComponent(typeof(PolyNavAgent))]
public class ClickToMove : MonoBehaviour
{

    private PolyNavAgent _agent;
    private PolyNavAgent agent
    {
        get { return _agent != null ? _agent : _agent = GetComponent<PolyNavAgent>(); }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Camera.main.ScreenToWorldPoint(Input.mousePosition).y > 0)
        {
            agent.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
                if (touchPosition.y > 0)
                {
                    agent.SetDestination(touchPosition);
                }
            }
        }
    }
}