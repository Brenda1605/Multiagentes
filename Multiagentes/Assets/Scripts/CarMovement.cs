using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarMovement : MonoBehaviour
{
    NavMeshAgent car;
    public Transform object;

    // Start is called before the first frame update
    void Start()
    {
        car = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        car.destination = object.position;
    }
}

