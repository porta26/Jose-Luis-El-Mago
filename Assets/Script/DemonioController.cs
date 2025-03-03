using UnityEngine;

public class DemonioController : MonoBehaviour
{

    [SerializeField] float demonioSpeed;
    [SerializeField] GameObject jugador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (jugador != null)
        {
            // Calcula la dirección hacia el jugador
            Vector2 direccion = (jugador.transform.position - transform.position).normalized;

            // Mueve el objeto hacia el jugador
            transform.position += (Vector3)direccion * demonioSpeed * Time.deltaTime;

            // Voltear en el eje Y según la posición del jugador
            if (direccion.x < 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                transform.rotation = Quaternion.Euler(0, 180, 0);

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damage"))
        {
            Destroy(gameObject);
        }
    }
}
