using UnityEngine;

public class SparringController : MonoBehaviour
{
    SpriteRenderer sp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        ChangeColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damage"))
        {
            ChangeColor();
        }
    }

    void ChangeColor(){
        Color color =new Color(Random.Range(0,1f),Random.Range(0,1f),Random.Range(0,1f));
        sp.color = color;
        Debug.Log(color);
    }
}
