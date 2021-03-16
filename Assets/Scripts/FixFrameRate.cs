using UnityEngine;

public class FixFrameRate : MonoBehaviour
{
    void Start(){
        Application.targetFrameRate = 30;
    }
}

