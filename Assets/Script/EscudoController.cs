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
        Debug.Log("Algo toco");
        Vector3 impulso = -(jugador.transform.position - collision.transform.position);
        impulso += Vector3.up;

        if (collision.GetComponent<Rigidbody2D>() != null)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.AddForce(impulso * fuerzaImpulso);
        }

    }
}

