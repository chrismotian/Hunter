using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIndefinitely : MonoBehaviour
{
    //This is a generic script that can be used to spawn anything from power-ups to enemies!

    [SerializeField] GameObject[] obj;
    [SerializeField] float spawnMin = 1f;
    [SerializeField] float spawnMax = 2f;

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        GameObject instance = (GameObject)Instantiate(obj[Random.Range(0, obj.GetLength(0))], transform.position, Quaternion.identity);
        instance.transform.parent = this.transform;
        Invoke("Spawn", Random.Range(spawnMin, spawnMax));
    }
}
