using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movimientodeljugador : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 10f;
    public float fuerzaRebote = 10f;
    public float longitudRaycast = 0.1f; // linea que registra que esta en el suelo 
    public LayerMask capaSuelo; // es el leyer que creamos en el leyer 
    public int vida = 3;
   
    private bool enSuelo; // se hace por que el personaje necesita el suelo para saltar 
    private bool recibiendoDanio;
    private bool atacando;
    public bool muerto;
    
 
    
    private Rigidbody2D rb; // se llama al rigibody para poder usarlo y hacer que salte 

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!muerto)
        {
            if (!atacando)
             {
                Movimiento();
                // Movimiento en el eje X
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);
                enSuelo = hit.collider != null;

                if (enSuelo && Input.GetKeyDown(KeyCode.Space) && !recibiendoDanio)
                {
                    rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
                }
            }

            if (Input.GetKeyDown(KeyCode.Z) && !atacando && enSuelo)
            {
                Atacando();
            }
        }

        animator.SetBool("ensuelo", enSuelo);
        animator.SetBool("recibeDanio", recibiendoDanio);
        animator.SetBool("Atacando", atacando);
        animator.SetBool("muerto", muerto);
    }

    public void Movimiento()
    {
        float velocidadX = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;
        float velocidadY = Input.GetAxis("Vertical") * Time.deltaTime * velocidad;

        animator.SetFloat("movement", velocidadX * velocidad); // movement es el nombre puede variar dependiendo como se le ponga en el animator


        Vector3 posicion = transform.position;

        if (!recibiendoDanio)
            transform.position = new Vector3(velocidadX + posicion.x, velocidadY + posicion.y, posicion.z);


    }
    public void RecibeDanio(Vector2 direccion, int cantDanio) //esta fuera del update por que solamente se va ejecutar cuando el enemigo choque con el jugador 
    {
        if(!recibiendoDanio)
        { 
        recibiendoDanio = true;
            vida -= cantDanio;
            if (vida <=0)
            {
                muerto = true;
            }
            if (!muerto)
            {
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized; //resta muestra posicion menos la posicion de la que fuimos atacados 
        rb.AddForce(rebote* fuerzaRebote, ForceMode2D.Impulse);
        }
      }
    }
    public void DesactivaDanio()
    {
        recibiendoDanio = false;
        rb.linearVelocity = Vector2.zero;
    }

    public void Atacando()
    {
        atacando = true;
    }

    public void DesactivaAtaque()
    {
        atacando = false;
    }
    void OnDrawGizmos()// figuras imaginarias que solo se ven en el editor 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
        
    }
}