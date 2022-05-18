using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAct : MonoBehaviour
{
    Vector3 position;

    void Start() 
    {
        position = transform.position;
    }

    void Update() 
    {
        position.x += 1*Time.deltaTime;

        transform.position = position;
    }
}
