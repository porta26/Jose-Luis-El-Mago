using UnityEngine;

public class GordoController : MonoBehaviour
{
    [SerializeField] float gordoSpeed = 1;
    Vector2 movimiento = Vector2.left;

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
}
