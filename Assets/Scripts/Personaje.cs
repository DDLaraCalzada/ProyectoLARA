using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    public float jumpForce = 10f; // Fuerza del salto
    public float speed = 5f;
    private Rigidbody rb; // Referencia al Rigidbody

    private Vector2 startTouchPosition; // Posición inicial del toque
    private Vector2 endTouchPosition; // Posición final del toque

    public GameObject suelo;
    public Transform final_suelo; // Posición final del suelo inicial
    private Vector3 nextSpawnPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtener la referencia al Rigidbody

        // Inicializar nextSpawnPosition con la posición inicial del suelo
        nextSpawnPosition = final_suelo.position;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            StartCoroutine(SpawnNextGroundWithDelay());
        }
    }

    private IEnumerator SpawnNextGroundWithDelay()
    {
        // Instanciar un nuevo suelo en la posición siguiente
        GameObject newGround = Instantiate(suelo, nextSpawnPosition, Quaternion.identity);

        // Esperar 2 segundos antes de calcular la posición del próximo suelo
        yield return new WaitForSeconds(2f);

        // Buscar el Renderer en el nuevo suelo o en sus hijos y subhijos
        Renderer renderer = newGround.GetComponent<Renderer>();
        if (renderer == null)
        {
            renderer = newGround.GetComponentInChildren<Renderer>();
        }

        // Verificar si se encontró un Renderer
        if (renderer != null)
        {
            // Calcular la posición del próximo suelo basado en la longitud del suelo actual
            float sueloLength = renderer.bounds.size.x;
            nextSpawnPosition += new Vector3(sueloLength, 0, 0);
        }
        else
        {
            Debug.LogError("No se encontró un componente Renderer en el suelo instanciado o sus hijos.");
        }
    }

}
