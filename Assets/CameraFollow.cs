using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Jugador")]
    public Transform jugador;

    [Header("Suavizado")]
    public float suavizado = 5f;

    [Header("Posición de la cámara")]
    public float altura = 0f;
    public float distancia = -10f;

    [Header("Límites del escenario")]
    public float limiteIzquierdo = 0f;
    public float limiteDerecho = 20f;

    void LateUpdate()
    {
        if (jugador == null)
            return;

        float posicionX = Mathf.Clamp(
            jugador.position.x,
            limiteIzquierdo,
            limiteDerecho
        );

        Vector3 posicionObjetivo = new Vector3(
            posicionX,
            altura,
            distancia
        );

        transform.position = Vector3.Lerp(
            transform.position,
            posicionObjetivo,
            suavizado * Time.deltaTime
        );
    }
}