using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HabilidadBehaviour : StateMachineBehaviour

{
    [SerializeField] private GameObject habilidad;
   [SerializeField] private float offsetY;
   private Jefe jefe;
    private Transform jugador;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
       jefe = animator.GetComponent<Jefe>();
       jugador = jefe.jugador;

        jefe.MirarJugador();

        Vector2 posicionAparicion = new Vector2(jugador.position.x, jugador.position.y + offsetY);
       Instantiate(habilidad, posicionAparicion, Quaternion.identity);
    }
}
