using UnityEngine;

public class SlimeFollow : MonoBehaviour
{
    public Transform jugador;
    public float velocidad = 1.5f;
    public float distancia = 6f;

    void Update()
    {
        if (jugador == null) return;

        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);

        if (distanciaJugador <= distancia)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(jugador.position.x, transform.position.y),
                velocidad * Time.deltaTime
            );
        }
    }
}