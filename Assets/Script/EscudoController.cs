using UnityEngine;

public class EscudoController : MonoBehaviour
{
    [SerializeField] GameObject jugador;
    [SerializeField] float fuerzaImpulso = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Damage") && !collision.CompareTag("Player")&& !collision.CompareTag("Suelo")&& !collision.CompareTag("Limite"))
        {
            Debug.Log("Algo toco");
            Vector3 impulso = -(jugador.transform.position - collision.transform.position);
            impulso += Vector3.up;
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.AddForce(impulso * fuerzaImpulso);
        }
    }
}
