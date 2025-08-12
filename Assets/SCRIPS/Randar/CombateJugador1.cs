using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CombateJugador1 : MonoBehaviour
{
    [SerializeField] private float vida;
 
    public void TomarDaño(float daño)
    {
        vida -= daño;

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
