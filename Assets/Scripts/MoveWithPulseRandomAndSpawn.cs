using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPulseRandomAndSpawn : MonoBehaviour
{
    [SerializeField] bool spawn = true;
    GameObject instance = null;
    [SerializeField] GameObject spawnGameObject = null;
    // Start is called before the first frame update
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
    [SerializeField] float pulse = 2;
    float time = 2;
    float period = 0;
    [SerializeField] Vector2 target = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        startX = this.transform.position.x;
        startY = this.transform.position.y;
        target = new Vector2(startX + (int)(Random.Range(1, 11) * Random.Range(1, areaY + 1)) * 0.1f, startY + (int)(Random.Range(1, 11) * Random.Range(1, areaY + 1)) * 0.1f);
        if (spawn) Spawn(target);
        if (Vector2.Distance(this.transform.position, target) > 0)
        {
            directionList = CalculateDirection();
            directionList.Reverse();
        }
        else
        {
            targetReached = true;
        }
        period = directionList.Count / pulse;
        time = 1 / period;
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
                if (directionList.Count == 0)
                {
                    targetReached = true;
                    Destroy(instance);
                }
                time = 1 / period;
            }
        }
        else
        {
            targetReached = false;
            target = new Vector2(startX + (int)(Random.Range(1, 11) * Random.Range(1, areaY + 1)) * 0.1f, startY + (int)(Random.Range(1, 11) * Random.Range(1, areaY + 1)) * 0.1f);
            if (spawn) Spawn(target);
            directionList = CalculateDirection();
            directionList.Reverse();
            period = directionList.Count / pulse;
            time = 1 / period;
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

        Gizmos.DrawSphere(target, 0.1f);
    }

    void Spawn(Vector2 targetPosition)
    {
        instance = (GameObject)Instantiate(spawnGameObject, targetPosition, Quaternion.identity);
        Invoke("Spawn", 0);
    }
}
