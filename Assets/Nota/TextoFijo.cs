using UnityEngine;

public class CanvasFijo : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    private Vector3 escalaInicial;

    void Start()
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
        escalaInicial = transform.localScale;
    }

    void LateUpdate()
    {
        transform.position = posicionInicial;
        transform.rotation = rotacionInicial;
        transform.localScale = escalaInicial;
    }
}