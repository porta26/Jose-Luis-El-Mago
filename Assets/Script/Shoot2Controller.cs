using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Shoot2Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private CircleCollider2D cc;
    [SerializeField] float explosionTime;
    [SerializeField] Light2D lightShoot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player") && !col.CompareTag("Escudo"))
        {
            rb.mass = 0;
            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.zero;
            Destroy(gameObject, explosionTime);
            transform.localScale = new Vector3(2, 2, 0);
            transform.localScale = new Vector3(2, 2, 1f);
            CambiarRadio(1, 3);
            sp.color = Color.yellow;
        }
    }
    public void CambiarRadio(float radioInterior, float radioExterior)
    {
        if (lightShoot != null)
        {
            lightShoot.pointLightInnerRadius = Mathf.Max(0, radioInterior);
            lightShoot.pointLightOuterRadius = Mathf.Max(radioInterior, radioExterior);
        }
    }
}
