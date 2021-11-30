using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    private GameObject[] cars;
    private CarMovement carMovement;

    // Start is called before the first frame update
    void Start()
    {
        cars = new GameObject[5];

        for(int i = 0; i < 5; i++)
        {
            cars[i] = Instantiate(carPrefab, new Vector3(-140.6434f+i*10, 0.5017449f, 1.359884f), Quaternion.identity);
        }

        carMovement = cars[4].GetComponent<CarMovement>();
        carMovement.dest = new Vector3(10f, 0f, 1.5f);
    }
}
