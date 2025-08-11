using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PausarJuego : MonoBehaviour
{
    public GameObject menuPausa;
    public bool JuegoPausado = false; // sirve para que sepa cuando hay que reanudar 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //aqui definimos la tecla con la que se va a pausar
        {
            if (JuegoPausado) // aqui declaramos que cuando el juego este pausado lo que hara sera reanudarlo 
            {
                Reanudar();
            }
            else // aqui declaramos lo contrario si no esta pausado lo pausara
            {
                Pausar();
            }
        }
    }
    public void Reanudar()
    {
        menuPausa.SetActive(false); // aqui le decimos que se desactive automaticamente 
        Time.timeScale = 1; // time sacale es la velocidad del tiempo a la cual se ejecuta 
        JuegoPausado = false;
    }

    public void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0;
        JuegoPausado = true;
    }

}
