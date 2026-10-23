using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 67f;
    public float fuerzaSalto = 5f;

    [Header("Energía")]
    public int energia = 100;

    private Rigidbody2D rb;
    private Animator animator;
    private float movimientoX;
    private bool enSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Update()
    {
        movimientoX = 0f;

        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.LeftArrow))
            movimientoX = -1f;
        else if (Input.GetKey(KeyCode.D) ||
                 Input.GetKey(KeyCode.RightArrow))
            movimientoX = 1f;

        if (animator != null)
            animator.SetFloat("Speed", Mathf.Abs(movimientoX));

        if ((Input.GetKeyDown(KeyCode.Space) ||
             Input.GetKeyDown(KeyCode.UpArrow)) && enSuelo)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x, fuerzaSalto);

            enSuelo = false;

            if (animator != null)
                animator.SetTrigger("Jump");
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            movimientoX * velocidad,
            rb.linearVelocity.y);
    }

    public void RecibirDano(int energiaPerdida, float nuevaVelocidad)
    {
        energia = Mathf.Max(0, energia - energiaPerdida);

        // La velocidad nunca aumenta al tocar otro enemigo.
        velocidad = Mathf.Min(velocidad, nuevaVelocidad);

        Debug.Log("Energía: " + energia +
                  " | Velocidad: " + velocidad);
    }

    public void RecuperarEnergia()
    {
        energia = 100;
        velocidad = 67f;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contacto in collision.contacts)
        {
            if (contacto.normal.y > 0.5f)
            {
                enSuelo = true;
                return;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        enSuelo = false;
    }
}