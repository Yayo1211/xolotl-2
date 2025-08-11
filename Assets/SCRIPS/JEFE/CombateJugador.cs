using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CombateJugador : MonoBehaviour
{
    [SerializeField] private float vida;
 
    public void TomarDa�o(float da�o)
    {
        vida -= da�o;

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
