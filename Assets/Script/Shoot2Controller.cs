using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Shoot2Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private CircleCollider2D cc;
    [SerializeField] float explosionTime;
    [SerializeField] float explosionTamaño = 3;
    [SerializeField] Sprite spriteExplosion;
    [SerializeField] Light2D lightShoot;
    bool escalo = false;

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
        if (!escalo)
        {
            rb.mass = 0;
            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.zero;
            Destroy(gameObject, explosionTime);
            transform.localScale *= explosionTamaño;
            CambiarRadio(1, 3);

            sp.sprite = spriteExplosion;
            Vector2[] puntos = GetComponent<PolygonCollider2D>().points;
            for (int i = 0; i < puntos.Length; i++)
            {
                puntos[i] *= explosionTamaño;
            }
            GetComponent<PolygonCollider2D>().points = puntos;
            escalo = true;
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
