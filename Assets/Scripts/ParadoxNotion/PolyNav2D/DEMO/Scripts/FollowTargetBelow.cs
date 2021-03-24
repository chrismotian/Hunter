using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyNav;

//example
[RequireComponent(typeof(PolyNavAgent))]
public class FollowTargetBelow : MonoBehaviour
{

    public Transform target;

    private PolyNavAgent _agent;
    private PolyNavAgent agent
    {
        get { return _agent != null ? _agent : _agent = GetComponent<PolyNavAgent>(); }
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(new Vector2 (target.position.x-0.1f, target.position.y-2));
        }
    }
}