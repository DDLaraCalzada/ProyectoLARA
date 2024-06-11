using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    public float jumpForce = 10f; // Fuerza del salto
    public float speed = 5f;
    private Rigidbody rb; // Referencia al Rigidbody2D del personaje

    private Vector2 startTouchPosition; // Posición inicial del toque
    private Vector2 endTouchPosition; // Posición final del toque

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtener la referencia al Rigidbody2D
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        DelizSalto();
    }

    void DelizSalto()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Comenzar a detectar el toque
            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }

            // Detectar cuando se levanta el toque
            if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;

                // Verificar que el toque comenzó en la parte izquierda de la pantalla
                if (startTouchPosition.x < Screen.width / 2)
                {
                    // Verificar si el deslizamiento fue hacia arriba
                    if (endTouchPosition.y > startTouchPosition.y)
                    {
                        Jump();
                    }
                }
            }
        }
    }

    void Jump()
    {
        // Aplicar una fuerza hacia arriba para hacer que el personaje salte
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
