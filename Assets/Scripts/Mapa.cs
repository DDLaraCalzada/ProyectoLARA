using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapa : MonoBehaviour
{
    public GameObject[] obstaculos;
    public Transform[] posiciones;
    public string playerTag = "Player"; 
    public float distanciaEliminacion = 10; 

    private Transform player;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("No se encontró ningún objeto con la etiqueta " + playerTag);
            return;
        }

        foreach (Transform ob in posiciones)
        {
            GameObject objectToSpawn = obstaculos[Random.Range(0, obstaculos.Length)];

            GameObject poner = Instantiate(objectToSpawn, ob.position, Quaternion.identity);

            StartCoroutine(VerificarDistanciaYEliminar(poner));
        }
    }

    private IEnumerator VerificarDistanciaYEliminar(GameObject objeto)
    {
        while (true)
        {
            yield return null;

            if (objeto.transform.position.x < player.position.x - distanciaEliminacion)
            {
                Destroy(objeto);
                yield break;
            }
        }
    }

}
