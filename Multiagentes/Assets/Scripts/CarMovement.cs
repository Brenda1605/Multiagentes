using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarMovement : MonoBehaviour
{
    public Vector3 dest;
    public int id;

    void Start()
    {
        dest = Vector3.zero;
    }

    void Update() {
        if(dest != Vector3.zero){
            transform.position = Vector3.MoveTowards(transform.position, dest, Time.deltaTime*3);
        }
    }
}

