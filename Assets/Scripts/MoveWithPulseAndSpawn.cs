using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPulseAndSpawn : MonoBehaviour
{
    [SerializeField] bool spawn = true;
    GameObject instance = null;
    [SerializeField] GameObject spawnGameObject = null;
    int currentIndex;
    bool plusY = true;
    bool plusX = true;
    bool targetReached = false;
    List<bool> directionList = null;
    [SerializeField] float pulse = 2;
    float time = 2;
    float period = 0;
    [SerializeField] List<Vector2> waypoints = null;
    // Start is called before the first frame update
    void Start()
    {
        if (spawn) Spawn(waypoints[0]);
        if (Vector2.Distance(this.transform.position, waypoints[0]) > 0)
        {
            directionList = CalculateDirection(0);
            directionList.Reverse();
        }
        else
        {
            targetReached = true;
        }
        period =   directionList.Count / pulse;
        time = 1/period;
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetReached)
        {
            time = time - Time.deltaTime;
            if (time < 0)
            {
                if (directionList[directionList.Count - 1])
                {
                    if (plusX)
                    {
                        transform.position = new Vector2(transform.position.x + 0.1f, transform.position.y);
                    }
                    else
                    {
                        transform.position = new Vector2(transform.position.x - 0.1f, transform.position.y);
                    }
                }
                else
                {
                    if (plusY)
                    {
                        transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
                    }
                    else
                    {
                        transform.position = new Vector2(transform.position.x, transform.position.y - 0.1f);
                    }
                }
                directionList.RemoveAt(directionList.Count - 1);
                if(directionList.Count == 0)
                {
                    targetReached = true;
                    Destroy(instance);
                }
                time = 1 / period;
            }
        }
        else
        {
            currentIndex = (int)Mathf.Repeat(currentIndex + 1, waypoints.Count);
            if (spawn) Spawn(waypoints[currentIndex]);
            if (Vector2.Distance(this.transform.position, waypoints[currentIndex]) > 0)
            {
                targetReached = false;
                directionList = CalculateDirection(currentIndex);
                directionList.Reverse();
            }
            else
            {
                targetReached = true;
            }
            period = directionList.Count / pulse;
            time = 1 / period;
        }
    }
    List<bool> CalculateDirection(int index)
    {
        if (waypoints[index].x > this.transform.position.x)
        {
            plusX = true;
        }
        else if (waypoints[index].x < this.transform.position.x)
        {
            plusX = false;
        }
        if (waypoints[index].y > this.transform.position.y)
        {
            plusY = true;
        }
        else if (waypoints[index].y < this.transform.position.y)
        {
            plusY = false;
        }
        float unitDiffX = Mathf.Abs(waypoints[index].x - this.transform.position.x);
        float unitDiffY = Mathf.Abs(waypoints[index].y - this.transform.position.y);
        List<bool> commandos = new List<bool>();
        Debug.Log("DeltaX = " + unitDiffX + " and DeltaY = " + unitDiffY);
        while (unitDiffX > 0 || unitDiffY > 0)
        {
            if (unitDiffX == unitDiffY)
            {
                for (int i = 0; i < unitDiffX / 0.1; i++)
                {
                    commandos.Add(true);
                    commandos.Add(false);
                }
                break;
            }
            if (unitDiffX > unitDiffY)
            {
                commandos.Add(true);
                unitDiffX = unitDiffX - 0.1f;
            }
            else
            {
                commandos.Add(false);
                unitDiffY = unitDiffY - 0.1f;
            }
        }

        return commandos;
    }

    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < waypoints.Count; i++)
        {
            Gizmos.DrawSphere(waypoints[i], 0.1f);
        }
    }
    void Spawn(Vector2 targetPosition)
    {
        instance = (GameObject)Instantiate(spawnGameObject, targetPosition, Quaternion.identity);
        Invoke("Spawn", 0);
    }
}
