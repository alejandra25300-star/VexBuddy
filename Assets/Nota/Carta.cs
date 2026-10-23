using UnityEngine;

public class CartaInteractuable : MonoBehaviour
{
    public GameObject notaAbierta;

    void Start()
    {
        notaAbierta.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            notaAbierta.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            notaAbierta.SetActive(false);
        }
    }
}