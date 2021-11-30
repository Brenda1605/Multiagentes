using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class Requesting : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private GameObject carGroup;
    private List<GameObject> cars;
    private CarMovement carMovement;
    private Hashtable carsHT;
    private Data d;
    private GameObject newCar;
    private HashSet<int> existingCars = new HashSet<int>();

    void Start() {
        StartCoroutine(Wait());
    }

    void CompareCars() {
        /*
        foreach(DictionaryEntry de in carsHT)
            Console.WriteLine("Key: {0}, Value: {1}", de.Key, de.Value);

        foreach(Position p in d.cars){
            if (carsHT.ContainsKey(p.id)) {
                int index = FindCar(p.id);
                if (index != -1) {
                    carMovement = cars[index].GetComponent<CarMovement>();
                    carMovement.transform.position = new Vector3(p.x*10f, p.z+1.3f, p.y*10f);
                }
            } else {
                carsHT.Add(p.id, p);
                newCar = Instantiate(carPrefab, new Vector3(p.x*10f, p.z+1.3f, p.y*10f), Quaternion.identity);
                carMovement = newCar.GetComponent<CarMovement>();
                carMovement.id = p.id;
                cars.Add(newCar);
            }
        }
        */
        foreach(Position p in d.cars) {
            if (existingCars.Contains(p.id)) {
                GameObject car = FindCar(p.id);
                carMovement = car.GetComponent<CarMovement>();
                carMovement.dest = new Vector3(p.x, 0, p.y);
            } else {
                newCar = Instantiate(carPrefab, new Vector3(p.x, 0, p.y), Quaternion.identity);
                carMovement = newCar.GetComponent<CarMovement>();
                carMovement.id = p.id;
                existingCars.Add(p.id);
                newCar.transform.parent = carGroup.transform;
            }
        }
    }
    
    GameObject FindCar(int id) {
        foreach(Transform car in carGroup.transform) {
            if (car.GetComponent<CarMovement>().id == id) {
                return car.gameObject;
            }
        }

        return new GameObject();
    }

    IEnumerator Wait() {
        while(true){
            StartCoroutine(GetPositions());
            yield return new WaitForSeconds(1);
        }
    }
 
    IEnumerator GetPositions() {
        UnityWebRequest www = UnityWebRequest.Get("https://2f1f-177-241-41-205.ngrok.io");
        yield return www.SendWebRequest();
 
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
        }

        d = JsonUtility.FromJson<Data>(www.downloadHandler.text);

        CompareCars();
    }
}
