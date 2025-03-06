using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoriaController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] textos;
    [SerializeField] private float duracion = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CambiarOpacidadTodos());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Menu");
        }
    }
    private IEnumerator CambiarOpacidadTodos()
    {
        for (int i = 0; i < textos.Length; i++)
        {
            Debug.Log("Iteracion: " + i);
            StartCoroutine(CambiarOpacidad(textos[i]));
            yield return new WaitForSeconds(duracion);
        }
    }
    // Coroutine para cambiar la opacidad
    private IEnumerator CambiarOpacidad(TextMeshProUGUI texto)
    {
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracion)
        {
            tiempoTranscurrido += Time.deltaTime; // Incrementa el tiempo

            // Calcular la opacidad actual en función del tiempo
            float nuevaOpacidad = Mathf.Lerp(0f, 1f, tiempoTranscurrido / duracion);

            // Establecer la nueva opacidad en el color del texto
            Color colorActual = texto.color;
            colorActual.a = nuevaOpacidad;
            texto.color = colorActual;

            yield return null; // Esperar al siguiente frame
        }

        // Asegurarse de que la opacidad sea 1 al final
        Color finalColor = texto.color;
        finalColor.a = 1f;
        texto.color = finalColor;

    }
}
