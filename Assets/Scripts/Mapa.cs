using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapa : MonoBehaviour
{
    public GameObject[] obstaculos;
    public Transform[] posiciones;


    void Start()
    {
        foreach (Transform ob in posiciones)
        {
            GameObject objectToSpawn = obstaculos[Random.Range(0, obstaculos.Length)];

            Instantiate(objectToSpawn, ob.transform.position, Quaternion.identity);
        }
    }


    void Update()
    {
        
    }
}
