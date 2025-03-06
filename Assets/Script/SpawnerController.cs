
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] GameObject demonioPrefab;
    [SerializeField] GameObject gordoPrefab;
    [SerializeField] GameObject avispaPrefab;
    [SerializeField] float demonioSpawnRate = 60f;
    [SerializeField] float avispaSpawnRate = 70f;
    [SerializeField] float gordoPuntuacion = 30f;
    float gordoUltimoSpawn = 0;
    [SerializeField] float spawnRate = 2;
    [SerializeField] float escalaDificultad = 100;
    [SerializeField] float spawnRateMinimo = 0.2f;
    [SerializeField] float spawnRateMaximo = 50f;
    float contador;
    [SerializeField] Vector3 spawnLeft;
    [SerializeField] Vector3 spawnRight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        contador = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        bool hayGordo = false;
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        foreach (GameObject g in enemigos)
        {
            if (g.name.Contains("Gordo"))
            {
                hayGordo = true;
                break;
            }
        }
        if (Time.time > contador + CalcularSpawnRate() && !hayGordo)
        {
            SpawnEnemigo();
            contador = Time.time;
        }
    }
    void FixedUpdate()
    {

    }
    void SpawnEnemigo()
    {
        int lado = Random.Range(0, 2);
        Vector3 spawnPoint = (lado == 0) ? spawnLeft : spawnRight;

        float enemigo = Random.Range(0, 101);
        Debug.Log("Enemigo: " + enemigo);

        if (GameManager.instancia.GetPuntuacion() >= gordoUltimoSpawn + gordoPuntuacion && GameManager.instancia.GetPuntuacion() >= gordoPuntuacion)
        {
            Instantiate(gordoPrefab, spawnPoint, Quaternion.identity);
            gordoUltimoSpawn = GameManager.instancia.GetPuntuacion();
        }
        else if (enemigo > avispaSpawnRate && GameManager.instancia.GetPuntuacion() >= 20) // 30% de probabilidad
        {
            Instantiate(avispaPrefab, spawnPoint, Quaternion.identity);
        }
        else
        {
            Instantiate(demonioPrefab, spawnPoint, Quaternion.identity);
        }
        Debug.Log("Puntuacion: " + GameManager.instancia.GetPuntuacion());

    }
    /*
    Esto es una funcion racional para representar el escalado de dificultad del juego.
        float spawnRate:                        el ángulo de la curva (cuanto menor mas rapido escala la dificultad)
        float spawnRateMaximo:                  desplazamiento en x (cuanto mayor mas enemigos al inicio)
        float spawnRateMinimo:                  desplazamiento en y (cuanto mayor menos enemigos al final)
        float escalaDificultad:                 lo mismo que el spawnRate (creo)
        GameManager.instancia.GetPuntuacion():  la puntuacion
    */
    float CalcularSpawnRate()
    {
        return (spawnRate / ((GameManager.instancia.GetPuntuacion() + spawnRateMaximo) / escalaDificultad)) + spawnRateMinimo;
    }
}
