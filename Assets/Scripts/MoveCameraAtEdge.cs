using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraAtEdge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.mousePosition.x > Screen.width - 10f)
        {
          //  Camera.main.transform.position.x += 10 * Time.deltaTime;
        }
        
    }
}
