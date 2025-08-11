using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
public class PasarNivel : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision) // esto se hace para que el jugador coliseone con el objeto y esto hace que cargue la ecena 
    {
        if(collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // este es para que sepa en que cena estamos y apartir de esta sumarle otra ecena y pasarnos a la nueva ecena
        }
    }
}
