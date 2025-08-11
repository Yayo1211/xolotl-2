using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    public float speed = 3; // variable de velocidad

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    void FixedUpdate() // se ocupa fixedupdate por que se utilizan fisicas para mover la bala 
    {
        rigidbody.MovePosition(transform.position + transform.right * speed * Time.fixedDeltaTime); // este es para mover la bala ya que recibe informacion de la posicion y hacia donde va la bala 
    }
}