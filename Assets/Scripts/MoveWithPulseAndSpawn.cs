using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPulseAndSpawn : MonoBehaviour
{
    [SerializeField] bool spawn = true;
    GameObject instance = null;
    [SerializeField] GameObject spawnGameObject = null;
    public int currentIndex;
    bool plusY = true;
    bool plusX = true;
    bool targetReached = false;
    List<bool> directionList = null;
    [SerializeField] float pulseTime = 2;
    float timePerStepTemp = 2;
    float timePerStep = 0;
    public List<Vector2> waypoints = null;

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
        timePerStep = pulseTime / directionList.Count;
        timePerStepTemp = timePerStep;
    }

    void Update()
    {
        if (!targetReached)
        {
            if (timePerStepTemp < 0)
            {
                Vector2 newPosition = Vector2.zero;
                int countList = directionList.Count;
                bool nextStepX = directionList[countList - 1];
                bool nextnextStepX = directionList[countList - 2];
                if ((nextStepX && !nextnextStepX) || (!nextStepX && nextnextStepX))
                {
                    if (plusX && plusY)
                    {
                        newPosition = PixelPerfectClamp(new Vector2(transform.position.x + 0.1f, transform.position.y + 0.1f));
                    }
                    else if (plusX && !plusY)
                    {
                        newPosition = PixelPerfectClamp(new Vector2(transform.position.x + 0.1f, transform.position.y - 0.1f));
                    }
                    else if (!plusX && plusY)
                    {
                        newPosition = PixelPerfectClamp(new Vector2(transform.position.x - 0.1f, transform.position.y + 0.1f));
                    }
                    else //if(!plusX && !plusY)
                    {
                        newPosition = PixelPerfectClamp(new Vector2(transform.position.x - 0.1f, transform.position.y - 0.1f));
                    }
                    directionList.RemoveAt(countList - 1);
                    directionList.RemoveAt(countList - 2);
                    timePerStepTemp = timePerStepTemp - timePerStep;
                }
                else if (nextStepX)
                {
                    if (plusX)
                    {
                        newPosition = PixelPerfectClamp(new Vector2(transform.position.x + 0.1f, transform.position.y));
                    }
                    else
                    {
                        newPosition = PixelPerfectClamp(new Vector2(transform.position.x - 0.1f, transform.position.y));
                    }
                    directionList.RemoveAt(countList - 1);
                }
                else //if(!nextStepX)
                {
                    if (plusY)
                    {
                        newPosition = PixelPerfectClamp(new Vector2(transform.position.x, transform.position.y + 0.1f));
                    }
                    else
                    {
                        newPosition = PixelPerfectClamp(new Vector2(transform.position.x, transform.position.y - 0.1f));
                    }
                    directionList.RemoveAt(countList - 1);
                }
                transform.position = newPosition;
                if (countList <= 3)
                {
                    targetReached = true;
                    Destroy(instance);
                    timePerStepTemp = timePerStepTemp - Time.deltaTime + (timePerStep * countList);
                }
                else
                {
                    timePerStepTemp = timePerStep - Time.deltaTime - timePerStepTemp;
                }
            }
            else
            {
                timePerStepTemp = timePerStepTemp - Time.deltaTime;
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
                }
            else
            {
                targetReached = true;
            }
            timePerStep = pulseTime / directionList.Count;
            timePerStepTemp = timePerStep - Time.deltaTime + timePerStepTemp;
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
                for (int i = 0; i < unitDiffX / 0.1f; i++)
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
    private Vector2 PixelPerfectClamp(Vector2 moveVector)
    {
        Vector2 vectorInPixels = new Vector2(
            Mathf.RoundToInt(moveVector.x * 10),
            Mathf.RoundToInt(moveVector.y * 10));

        return vectorInPixels / 10;

    }
}
