using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia { get; private set; }

    [SerializeField] float puntuacionMulti = 1;
    float puntuacion = 0;

    void Awake()
    {
        // Implementación del patrón Singleton
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject); // Evita que el GameManager se destruya al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Si ya existe una instancia, se destruye la nueva
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetPuntuacion()
    {
        return puntuacion * puntuacionMulti;
    }
    public void SumarPuntuacion(float puntuacionAñadida)
    {
        puntuacion += puntuacionAñadida;
    }
}
