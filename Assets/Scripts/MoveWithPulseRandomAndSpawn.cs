using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPulseRandomAndSpawn : MonoBehaviour
{
    [SerializeField] bool spawn = true;
    GameObject instance = null;
    [SerializeField] GameObject spawnGameObject = null;
    // area is defined by your start position.x +areaX and similar for y
    [SerializeField] float areaX = 4;
    [SerializeField] float areaY = 2;
    float startX = -2;
    float startY = -2;
    int currentIndex;
    bool plusY = true;
    bool plusX = true;
    bool targetReached = false;
    List<bool> directionList = null;
    [SerializeField] float pulseTime = 2;
    float timePerStepTemp = 2;
    float timePerStep = 0;
    [SerializeField] Vector2 target = Vector2.zero;

    void Start()
    {
        startX = this.transform.position.x;
        startY = this.transform.position.y;
        target = new Vector2(startX + (int)(Random.Range(1, 11) * Random.Range(1, areaY + 1)) * 0.1f, startY + (int)(Random.Range(1, 11) * Random.Range(1, areaY + 1)) * 0.1f);
        if (spawn) Spawn(target);
        if (Vector2.Distance(this.transform.position, target) > 0)
        {
            directionList = CalculateDirection();
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
            targetReached = false;
            target = new Vector2(startX + (int)(Random.Range(1, 11) * Random.Range(1, areaX + 1)) * 0.1f, startY + (int)(Random.Range(1, 11) * Random.Range(1, areaY + 1)) * 0.1f);
            if (spawn) Spawn(target);
            directionList = CalculateDirection();
            timePerStep = pulseTime / directionList.Count;
            timePerStepTemp = timePerStep - Time.deltaTime + timePerStepTemp;
        }
    }
    List<bool> CalculateDirection()
    {
        if (target.x > this.transform.position.x)
        {
            plusX = true;
        }
        else if (target.x < this.transform.position.x)
        {
            plusX = false;
        }
        if (target.y > this.transform.position.y)
        {
            plusY = true;
        }
        else if (target.y < this.transform.position.y)
        {
            plusY = false;
        }
        float unitDiffX = Mathf.Abs(target.x - this.transform.position.x);
        float unitDiffY = Mathf.Abs(target.y - this.transform.position.y);
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
        Gizmos.DrawSphere(target, 0.1f);
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

        return vectorInPixels /10 ;
            
    }
}
