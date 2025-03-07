using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia { get; private set; }

    [SerializeField] float puntuacionMulti = 1;
    [SerializeField] float puntuacionSegundo = 0.5f;
    [SerializeField] TextMeshProUGUI puntuacionText;
    [SerializeField] float vidasJugador = 3;
    [SerializeField] GameObject[] vidas;
    [SerializeField] TextMeshProUGUI textoPausa;
    [SerializeField] Camera camara;
    [SerializeField] TextMeshProUGUI textoMuerte;
    [SerializeField] Button reintentar;
    [SerializeField] TextMeshProUGUI reintentarText;
    [SerializeField] Button menu;
    [SerializeField] TextMeshProUGUI menuText;
    bool pausa = false;
    float puntuacion = 0;

    void Awake()
    {
        // Implementación del patrón Singleton
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject); // Si ya existe una instancia, se destruye la nueva
        }


    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camara = FindAnyObjectByType<Camera>();
        textoPausa.enabled = !textoPausa.enabled;
        textoMuerte.enabled = !textoMuerte.enabled;
        menu.interactable = !menu.interactable;
        menuText.enabled = !menuText.enabled;
        reintentar.interactable = !reintentar.interactable;
        reintentarText.enabled = !reintentarText.enabled;
        Time.timeScale = 1;
        StartCoroutine(PuntosTiempo());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && vidasJugador > 0)
        {
            pausa = !pausa;
            if (pausa)
            {
                textoPausa.enabled = !textoPausa.enabled;
                menu.interactable = !menu.interactable;
                menuText.enabled = !menuText.enabled;
                reintentar.interactable = !reintentar.interactable;
                reintentarText.enabled = !reintentarText.enabled;
                Time.timeScale = 0;
            }
            else
            {
                textoPausa.enabled = !textoPausa.enabled;
                menu.interactable = !menu.interactable;
                menuText.enabled = !menuText.enabled;
                reintentar.interactable = !reintentar.interactable;
                reintentarText.enabled = !reintentarText.enabled;
                Time.timeScale = 1;
            }
        }
    }
    void FixedUpdate()
    {
        if (vidasJugador <= 0)
        {
            textoMuerte.text = "Jose Luis ha sido derrotado con " + ((int)GetPuntuacion()).ToString() + " puntos (dinero de rentas)";
            textoMuerte.enabled = !textoMuerte.enabled;
            menu.interactable = !menu.interactable;
            menuText.enabled = !menuText.enabled;
            reintentar.interactable = !reintentar.interactable;
            reintentarText.enabled = !reintentarText.enabled;
            Time.timeScale = 0;
            camara.transform.position = new Vector3(0, 0, -10);
        }
    }
    public float GetPuntuacion()
    {
        return puntuacion * puntuacionMulti;
    }
    public void SumarPuntuacion(float puntuacionAñadida)
    {
        ActualizarPuntuacion();
        puntuacion += puntuacionAñadida;
    }
    public float GetVidas()
    {
        return vidasJugador;
    }
    public void SetVidas(float vidasNueva)
    {
        vidasJugador = vidasNueva;
        ActualizarVidas();
    }

    IEnumerator PuntosTiempo()
    {
        while (true)
        {
            SumarPuntuacion(puntuacionSegundo);
            yield return new WaitForSeconds(1);
        }

    }
    void ActualizarPuntuacion()
    {
        puntuacionText.text = ((int)GetPuntuacion()).ToString("D3");
    }
    void ActualizarVidas()
    {
        for (int i = 0; i < vidas.Length; i++)
        {
            if (i >= vidasJugador)
            {
                Debug.Log("Llama a convertir a grises");
                vidas[i].GetComponent<SpriteRenderer>().color = Color.grey;
            }
        }

    }
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
