using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigos : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5.0f; // sirve para que ele enemigo se mueve solo si esta serca de el
    public float speed = 2.0f;// velocidad con la que el enemigo se mueve hacia el jugador 
    public float fuerzaRebote = 10f;
    public int vida = 3;

    private Rigidbody2D rb; // este sirve para moverlo 
    private Vector2 movemet; // direccion hacia donde se vaya a mover, se tiene que llamar igual en el animator 
    private bool enMovimiento; // se tiene que llamar igual en animator 
    private bool recibiendoDanio;
    private bool playerVivo;
    private bool muerto;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerVivo = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerVivo && !muerto)
        {
            Movimiento();
        }
       
         animator.SetBool("enMovimiento", enMovimiento); // el parametro se tiene que llmar igual en el animator
         animator.SetBool("muerto", muerto);
    }
    private void Movimiento()
    { 
    float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius) // distancia hacia el jugador es menor que el radio del enemigo
        {
            Vector2 direction = (player.position - transform.position).normalized;

            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // aqui se cambia el -1 por 1 si el sprite esta alreves 
            }
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // aqui se cambia el 1 a -1 si el sprite esta alreves
            }


             movemet = new Vector2(direction.x, 0); // es un vector 2 por que el enemigo no vula solo va de izquierda a derecha 

            enMovimiento = true;
        }
        else //este es en caso de que la primera condicion no se cumple, si el jugador salio de la deteccion del enemigo entonces el enemigo no debera moverse
        {
            movemet = Vector2.zero;
            enMovimiento = false;
        }
        if (!recibiendoDanio)
        rb.MovePosition(rb.position + movemet * speed * Time.deltaTime);
 }
    private void OnCollisionEnter2D(Collision2D collision) // se activa cuando el colisinador choca con algo 
    {
        if (collision.gameObject.CompareTag("Player")) // nuestro personaje tiene que estar etiquetado y nombrado player 
        {
            Vector2 direccionDanio = new Vector2(transform.position.x, 0);
            Movimientodeljugador playerScript = collision.gameObject.GetComponent<Movimientodeljugador>();

            playerScript.RecibeDanio(direccionDanio, 1);
            playerVivo = !playerScript.muerto;
            if (!playerVivo)
            {
                enMovimiento = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("cuchillo")) // "cuchillo" se cambia el nombre si en el tag se llama diferente 
        {
            Vector2 direccionDanio = new Vector2(collision.gameObject.transform.position.x, 0);

            RecibeDanio(direccionDanio, 1); //  el 1 es la cantidad de da�o que hace
   
        }
    }
    public void RecibeDanio(Vector2 direccion, int cantDanio) //esta fuera del update por que solamente se va ejecutar cuando el enemigo choque con el jugador 
    {
        if (!recibiendoDanio)
        {
            vida -= cantDanio;
            recibiendoDanio = true;
            if (vida <= 0)
            {
                muerto = true;
                enMovimiento = false;
            }
            else
            {
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 0.2f).normalized;
                rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
                StartCoroutine(DesactivaDanio());
            }
        }
    }
    IEnumerator DesactivaDanio()
    {
        yield return new WaitForSeconds(0.4f);
        recibiendoDanio = false;
        rb.linearVelocity = Vector2.zero;
    }
    public void EliminarCuerpo()
    {
        Destroy(gameObject);
    }
    void OnDrawGizmosSelected() // es para ver el rango del enemigo
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
