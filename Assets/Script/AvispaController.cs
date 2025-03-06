using System.Collections;
using UnityEngine;

public class AvispaController : MonoBehaviour
{
    [SerializeField] float espera = 3f;
    [SerializeField] float minMovimiento = -5;
    [SerializeField] float maxMovimiento = 5;
    [SerializeField] float velocidadMovimiento = 2;
    [SerializeField] float precision = 2;
    [SerializeField] float velocidadDisparo = 5;
    [SerializeField] float projectileLife = 2;
    [SerializeField] GameObject jugador;
    [SerializeField] GameObject disparo;
    [SerializeField] float puntuacion = 5;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Mover());
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damage"))
        {
            Destroy(gameObject);
            GameManager.instancia.SumarPuntuacion(puntuacion);
        }
    }
    IEnumerator Mover()
    {
        Debug.Log("Se va a empezar a mover");
        while (true)
        {
            rb.linearVelocity = Vector2.zero;
            Debug.Log("Dispara y se mueve");
            GameObject proyectil = Instantiate(disparo, transform.position, Quaternion.identity);
            Vector3 direccion = ObjetivoDisparo() - transform.position;
            Rigidbody2D rbProyectil = proyectil.GetComponent<Rigidbody2D>();
            if (rbProyectil != null)
            {
                rbProyectil.linearVelocity = direccion.normalized * velocidadDisparo;
                proyectil.transform.Rotate(0, 0, Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg);
                Destroy(proyectil, projectileLife);
            }
            yield return new WaitForSeconds(espera / 2);
            rb.linearVelocity = new Vector3(Random.Range(minMovimiento, maxMovimiento), Random.Range(minMovimiento, maxMovimiento), 0).normalized * velocidadMovimiento;
            Debug.Log("Espera");
            yield return new WaitForSeconds(espera / 2);
        }
    }
    Vector3 ObjetivoDisparo()
    {
        int x = (Random.Range(0, 2) == 0) ? 1 : -1;
        int y = (Random.Range(0, 2) == 0) ? 1 : -1;
        float desviacionX = jugador.transform.position.x + Random.Range(0, precision) * x;
        float desviacionY = jugador.transform.position.y + Random.Range(0, precision) * y;
        Vector3 direccion = new Vector3(desviacionX, desviacionY, 0);

        return direccion;

    }
}
