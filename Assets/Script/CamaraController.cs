using UnityEngine;

public class CamaraController : MonoBehaviour
{
    // Variables
    [SerializeField] GameObject jugador;
    
    const float y = 0.0f;
    const float z = -10.0f;


    // Update is called once per frame
    void LateUpdate()
    {
        if (jugador != null)
        {
            Transform target = jugador.transform;
            // Solo sigue al personaje en el eje X
            Vector3 desiredPosition = new Vector3(target.position.x, y, z);
            transform.position = desiredPosition;
        }
    }
}
