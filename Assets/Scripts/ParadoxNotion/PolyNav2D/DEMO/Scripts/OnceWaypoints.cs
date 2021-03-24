using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyNav;

public class OnceWaypoints : MonoBehaviour
{
    public List<Vector2> WPoints = new List<Vector2>();
    private int currentIndex = -1;

    private PolyNavAgent _agent = null;

    private PolyNavAgent agent
    {
        get { return _agent != null ? _agent : _agent = GetComponent<PolyNavAgent>(); }
    }

    private void OnEnable()
    {
        agent.OnDestinationReached += MoveNext;
        agent.OnDestinationInvalid += MoveNext;
    }

    private void OnDisable()
    {
        agent.OnDestinationReached += MoveNext;
        agent.OnDestinationInvalid += MoveNext;
    }

    IEnumerator Start(){
        yield return new WaitForSeconds(1);
        if(WPoints.Count > 0){
            MoveNext();
        }
    }

    void MoveNext()
    {
        if (currentIndex < WPoints.Count - 1)
        {
            currentIndex = currentIndex + 1;
            agent.SetDestination(WPoints[currentIndex]);
        }
    }

    private void OnDrawGizmosSelected()
    {
        for(int i = 0; i < WPoints.Count; i++)
        {
            Gizmos.DrawSphere(WPoints[i], 0.1f);
        }
    }
}
