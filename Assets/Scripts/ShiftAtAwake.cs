using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftAtAwake : MonoBehaviour
{
    void Start()
    {
        int location = Random.Range(1, 7);
        switch (location)
        {
            case 1:
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 2);
                break;
            case 2:
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 1);
                break;
            case 3:
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
                break;
            case 4:
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 1);
                break;
            case 5:
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 2);
                break;
            case 6:
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 3);
                break;
        }
    }

}
