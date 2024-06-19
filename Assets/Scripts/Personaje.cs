using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    [Header ("Movimiento")]
    public float jumpForce = 10f; 
    public float speed = 5f;
    private Rigidbody rb;

    [Header("Pantalla")]
    private Vector2 startTouchPosition; 
    private Vector2 endTouchPosition;

    [Header ("Procedural")]
    public GameObject suelo;
    private Vector3 nextSpawnPosition;

    public int maxZonas = 2; // Máximo número de zonas permitidas
    private List<GameObject> zonasGeneradas = new List<GameObject>();

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nextSpawnPosition = new Vector3(-17.49f, -2.51f, 0);
        StartCoroutine(SpawnNextGroundWithDelay());
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

        // Agregar el suelo generado a la lista de zonas generadas
        zonasGeneradas.Add(newGround);

        // Verificar si hemos superado el límite máximo de zonas
        if (zonasGeneradas.Count > maxZonas)
        {
            // Obtener la primera zona generada (la más antigua)
            GameObject zonaMasAntigua = zonasGeneradas[0];

            // Eliminar la zona más antigua de la lista y de la escena
            zonasGeneradas.RemoveAt(0);
            Destroy(zonaMasAntigua);
        }


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

    }

}
