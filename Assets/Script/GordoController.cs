using UnityEngine;

public class GordoController : MonoBehaviour
{
    [SerializeField] float gordoSpeed = 1;
    [SerializeField] float vida = 15;
    Vector2 movimiento = Vector2.left;
    [SerializeField] float puntuacion = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)movimiento * gordoSpeed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Choco");
        if (collision.gameObject.CompareTag("Limite") || collision.gameObject.CompareTag("Torre"))
        {
            Debug.Log("Giro");
            movimiento *= -1;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damage"))
        {
            vida--;
            if (vida <= 0)
            {
                Destroy(gameObject);
                GameManager.instancia.SumarPuntuacion(puntuacion);
            }
        }
    }
}
