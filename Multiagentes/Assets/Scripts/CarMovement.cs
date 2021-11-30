using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent carMesh;
    public Vector3 dest;
    public int id;

    void Start()
    {
    }

    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, dest, Time.deltaTime);
    }
}

