using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            // Obtener la posici�n actual de la c�mara
            Vector3 cameraPosition = transform.position;

            // Solo actualizar la posici�n X de la c�mara si el jugador est� m�s adelante que minX
            if (player.position.x > 0)
            {
                cameraPosition.x = player.position.x;

            }

            transform.position = cameraPosition;

        }

    }
}
