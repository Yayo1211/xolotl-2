using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;


public class MenuSystem: MonoBehaviour
{
   public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego..."); // este es solo para mostrar que sirve ya que el otro solo se ve cuando el juego esta exportado 
        Application.Quit();
    }
}
