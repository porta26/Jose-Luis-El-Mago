using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Jugar()
    {
        SceneManager.LoadScene("Juego");
    }
     public void Salir()
    {
        Application.Quit();
        // Esto es solo para el editor de Unity, no se ejecutará en la versión final
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
