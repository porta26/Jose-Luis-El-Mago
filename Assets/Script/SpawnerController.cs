
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] GameObject demonioPrefab;
    [SerializeField] GameObject gordoPrefab;
    [SerializeField] GameObject avispaPrefab;
    [SerializeField] float demonioSpawnRate = 60f;
    [SerializeField] float avispaSpawnRate = 90f;
    [SerializeField] float gordoSpawnRate = 100f;
    [SerializeField] float spawnRate = 2;
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
        if (Time.time > contador + spawnRate)
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
        
        if (enemigo < demonioSpawnRate) // 60% de probabilidad
        {
            Instantiate(demonioPrefab,spawnPoint,Quaternion.identity);
        }
        else if (enemigo < avispaSpawnRate) // 30% de probabilidad
        {
            Instantiate(avispaPrefab,spawnPoint,Quaternion.identity);
        }
        else // 10% de probabilidad
        {
            Instantiate(gordoPrefab,spawnPoint,Quaternion.identity);
        }

    }
}
