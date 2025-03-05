using System.Collections;
using UnityEngine;

public class JugadorController : MonoBehaviour
{
    // Variables
    [SerializeField] float velocidad = 10.0f;
    [SerializeField] float fuerzaSalto = 5.0f;
    [SerializeField] GameObject disparoPrefab1;
    [SerializeField] float cooldown1 = 0.5f;
    float cooldown1Counter = 0;
    [SerializeField] GameObject disparoPrefab2;
    [SerializeField] float cooldown2 = 5f;
    float cooldown2Counter = 0;
    [SerializeField] GameObject escudo;
    [SerializeField] float escudoActivo = 2;
    [SerializeField] float cooldownEscudo = 8;
    float cooldownEscudoCounter = 0;
    [SerializeField] GameObject torre;
    [SerializeField] float torreActiva = 4;
    [SerializeField] float tamañoTorre = 30;
    [SerializeField] float torreCrecer = 3;
    [SerializeField] float cooldownTorre = 16;
    float cooldownTorreCounter = 0;
    [SerializeField] float projectileSpeed = 10.0f;
    [SerializeField] float projectileLife = 2.0f;
    [SerializeField] float vidas = 3;
    [SerializeField] Camera mainCamera;
    Rigidbody2D rb;
    bool isGrounded = false; // Variable para saber si el jugador está en el suelo
    bool inmune = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        escudo.SetActive(false);
        cooldown1Counter -= 100;
        cooldown2Counter -= 100;
        cooldownEscudoCounter -= 100;
        cooldownTorreCounter -= 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= cooldown1Counter + cooldown1)
        {
            Debug.Log("Clic0");
            Shoot1(Input.mousePosition);
            Debug.Log("Posición del ratón: " + Input.mousePosition);
            cooldown1Counter = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time >= cooldown2Counter + cooldown2)
        {
            Debug.Log("Clic1");
            Shoot2(Input.mousePosition);
            Debug.Log("Posición del ratón: " + Input.mousePosition);
            cooldown2Counter = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.E) && !escudo.activeSelf && Time.time >= cooldownEscudoCounter + cooldownEscudo)
        {
            Debug.Log("E");
            StartCoroutine(Escudo());

        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && Time.time >= cooldownTorreCounter + cooldownTorre)
        {
            Debug.Log("LeftControl");
            StartCoroutine(Torre());
            cooldownTorreCounter = Time.time;
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
        if (collision.gameObject.CompareTag("Suelo") || collision.gameObject.CompareTag("Torre"))
        {
            Debug.Log("Estoy en el suelo");
            isGrounded = true;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo") && !inmune)
        {
            vidas--;
            Debug.Log("Daño, Vidas restantes: " + vidas);
            StartCoroutine(PerderVida());
            if (vidas <= 0)
            {
                Destroy(gameObject);
            }
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
        escudo.transform.position = transform.position;
        yield return new WaitForSeconds(escudoActivo);
        escudo.SetActive(false);
        cooldownEscudoCounter = Time.time;
    }
    IEnumerator Torre()
    {
        GameObject torreInstancia = Instantiate(torre, transform.position + Vector3.down, Quaternion.identity);
        Vector3 escalaInicial = torre.transform.localScale;
        Vector3 escalaFinal = new Vector3(escalaInicial.x, escalaInicial.y * tamañoTorre, escalaInicial.z); // Crece en Y
        float tiempoTotal = 0;
        while (tiempoTotal < torreCrecer)
        {
            torreInstancia.transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, tiempoTotal / torreCrecer);
            tiempoTotal += Time.deltaTime;
            yield return null;
        }

        // Asegurar que llegue a la escala final exacta
        torreInstancia.transform.localScale = escalaFinal;

        // Esperar 2 segundos antes de destruir
        yield return new WaitForSeconds(torreActiva);
        Destroy(torreInstancia);
    }
    IEnumerator PerderVida()
    {
        inmune = true;
        Debug.Log("Animacion de perder vida");
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        float empiezaAnimacion = Time.time;
        while (Time.time < empiezaAnimacion + 1)
        {
            sp.enabled = !sp.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        sp.enabled = true;
        inmune = false;
    }
}