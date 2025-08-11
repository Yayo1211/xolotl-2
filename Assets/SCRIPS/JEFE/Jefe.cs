using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe : MonoBehaviour
{
    private Animator animator;
    private bool mirandoDerecha = true;

    public Rigidbody2D rb2D;
    public Transform jugador;

    [Header("Vida")]
    [SerializeField] private float vida;
    [SerializeField] private BarraDeVida barraDeVida;
    [Header("Ataque")]
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float dañoAtaque;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        barraDeVida.InicializarBarraDeVida(vida);

        GameObject jugadorGO = GameObject.FindGameObjectWithTag("Player");
        if (jugadorGO != null)
        {
            jugador = jugadorGO.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró al jugador con la etiqueta 'Player'.");
        }
    }

    private void Update()
    {
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);
        animator.SetFloat("distanciaJugador", distanciaJugador);
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        barraDeVida.CambiarVidaActual(vida);

        if (vida <= 0)
        {
            animator.SetTrigger("Muerte");
        }
    }

    // Llamada desde la animación cuando termina el trigger "Muerte"
    private void Muerte()
    {
        Destroy(gameObject);
    }

    public void MirarJugador()
    {
        if (jugador == null) return;

        if ((jugador.position.x > transform.position.x && !mirandoDerecha) ||
            (jugador.position.x < transform.position.x && mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }

    public void Ataque()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);

        foreach (Collider2D colision in objetos)
        {
            if (colision.CompareTag("Player"))
            {
                colision.GetComponent<CombateJugador>().TomarDaño(dañoAtaque);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
    }
}

