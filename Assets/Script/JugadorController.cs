using UnityEngine;

public class JugadorController : MonoBehaviour
{
    // Variables
    [SerializeField] float velocidad = 10.0f;
    [SerializeField] float fuerzaSalto = 5.0f;
    Rigidbody2D rb;
    bool isGrounded = false; // Variable para saber si el jugador está en el suelo

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            Debug.Log("Salto");
            isGrounded = false;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, fuerzaSalto);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Derecha");
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Izquierda");
            transform.Translate(Vector3.left * velocidad * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Suelo"))
        {
            Debug.Log("Estoy en el suelo");
            isGrounded = true;
        }
    }
}
