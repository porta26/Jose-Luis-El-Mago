using System.Collections;
using UnityEngine;

public class JugadorController : MonoBehaviour
{
    // Variables
    [SerializeField] float velocidad = 10.0f;
    [SerializeField] float fuerzaSalto = 5.0f;
    [SerializeField] GameObject disparoPrefab1;
    [SerializeField] float cooldown1 = 0.5f;
    float cooldown1Counter=0;
    [SerializeField] GameObject disparoPrefab2;
    [SerializeField] float cooldown2 = 5f;
    float cooldown2Counter=0;
    [SerializeField] GameObject escudo;
    [SerializeField] float escudoActivo = 2;
    [SerializeField] float cooldownEscudo = 5;
    float cooldownEscudoCounter = 0;
    [SerializeField] float projectileSpeed = 10.0f;
    [SerializeField] float projectileLife = 2.0f;
    [SerializeField] Camera mainCamera;
    Rigidbody2D rb;
    bool isGrounded = false; // Variable para saber si el jugador está en el suelo

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        escudo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= cooldown1Counter+cooldown1)
        {
            Debug.Log("Clic0");
            Shoot1(Input.mousePosition);
            Debug.Log("Posición del ratón: " + Input.mousePosition);
            cooldown1Counter = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time >= cooldown2Counter+cooldown2)
        {
            Debug.Log("Clic1");
            Shoot2(Input.mousePosition);
            Debug.Log("Posición del ratón: " + Input.mousePosition);
            cooldown2Counter = Time.time;
        }
        if(Input.GetKeyDown(KeyCode.E) && !escudo.activeSelf && Time.time >= cooldownEscudoCounter+cooldownEscudo)
        {
            StartCoroutine(Escudo());

        }
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            Debug.Log("Salto");
            isGrounded = false;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, fuerzaSalto);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {   
            transform.Translate(Vector3.left * -velocidad * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            Debug.Log("Estoy en el suelo");
            isGrounded = true;
        }
    }
    void Shoot1(Vector3 mouse)
    {
        // Crear el proyectil
        GameObject projectile = Instantiate(disparoPrefab1, transform.position, Quaternion.identity);
        // Obtener la dirección del disparo    
        Vector3 targetPos = mainCamera.ScreenToWorldPoint(mouse);
        Vector3 direction = (targetPos - transform.position);

        // Normalizar la dirección
        direction = direction / Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);


        // Aplicar velocidad al proyectil
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
            projectile.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            Destroy(projectile, projectileLife);
        }
    }
    void Shoot2(Vector3 mouse)
    {
        GameObject projectile = Instantiate(disparoPrefab2, transform.position, Quaternion.identity);
        // Obtener la dirección del disparo    
        Vector3 targetPos = mainCamera.ScreenToWorldPoint(mouse);
        Vector3 direction = (targetPos - transform.position);

        // Normalizar la dirección
        direction = direction * 0.2F;

        // Aplicar velocidad al proyectil
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
            projectile.transform.Rotate(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        } 
    }
    IEnumerator Escudo()
    {
        escudo.SetActive(true);
        yield return new WaitForSeconds(escudoActivo);
        escudo.SetActive(false);
        cooldownEscudoCounter = Time.time;
    }
}