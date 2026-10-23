using UnityEngine;

public class EnemySlime1 : MonoBehaviour
{
    public Transform jugador;
    public float velocidad = 1.5f;
    public float distanciaDeteccion = 6f;
    public float distanciaAtaque = 1f;
    public float tiempoEntreAtaques = 1.5f;

    private Rigidbody2D rb;
    private Animator animator;
    private float temporizadorAtaque;
    private bool muerto;
    private bool yaGolpeo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (rb != null)
        {
            rb.freezeRotation = true;
            rb.gravityScale = 1f;
        }

        BuscarJugador();
    }

    void BuscarJugador()
    {
        if (jugador == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) jugador = p.transform;
        }
    }

    void Update()
    {
        if (muerto) return;
        if (jugador == null) BuscarJugador();
        if (jugador == null || rb == null) return;

        if (temporizadorAtaque > 0)
            temporizadorAtaque -= Time.deltaTime;

        float dx = jugador.position.x - transform.position.x;
        float distancia = Vector2.Distance(transform.position, jugador.position);

        if (distancia > distanciaDeteccion)
            Detenerse();
        else if (distancia <= distanciaAtaque)
            Atacar();
        else
            Perseguir(dx);
    }

    void Perseguir(float dx)
    {
        float dir = Mathf.Sign(dx);
        rb.linearVelocity = new Vector2(dir * velocidad, rb.linearVelocity.y);

        if (animator != null)
            animator.SetFloat("Speed", velocidad);

        Girar(dir);
    }

    void Detenerse()
    {
        if (rb != null)
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        if (animator != null)
            animator.SetFloat("Speed", 0);
    }

    void Atacar()
    {
        Detenerse();

        if (temporizadorAtaque <= 0)
        {
            if (animator != null)
                animator.SetTrigger("Attack");

            temporizadorAtaque = tiempoEntreAtaques;
        }
    }

    void Girar(float dir)
    {
        if (dir == 0) return;
        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * dir;
        transform.localScale = escala;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (muerto || !collision.gameObject.CompareTag("Player"))
            return;

        PlayerMovement p = collision.gameObject.GetComponent<PlayerMovement>();
        Rigidbody2D jugadorRb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (p == null || jugadorRb == null) return;

        foreach (ContactPoint2D c in collision.contacts)
        {
            if (c.normal.y < -0.5f && jugadorRb.linearVelocity.y <= 0)
            {
                Morir();
                jugadorRb.linearVelocity =
                    new Vector2(jugadorRb.linearVelocity.x, 6f);
                return;
            }
        }

        if (!yaGolpeo)
        {
            yaGolpeo = true;
            p.RecibirDano(27, 34f);
        }
    }

    void Morir()
    {
        if (muerto) return;
        muerto = true;
        Detenerse();

        if (animator != null)
            animator.SetTrigger("Dead");

        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D col in cols)
            col.enabled = false;

        Destroy(gameObject, 1.5f);
    }
}