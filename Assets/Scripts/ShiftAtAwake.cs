using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftAtAwake : MonoBehaviour
{
    void Start()
    {
        int location = Random.Range(1, 4);
        switch (location)
        {
            case 1:
                this.transform.position = new Vector2(this.transform.position.x + 1, this.transform.position.y);
                break;
            case 2:
                this.transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y);
                break;
            case 3:
                this.transform.position = new Vector2(this.transform.position.x - 1, this.transform.position.y - 2);
                break;
        }
    }

}
