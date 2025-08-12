using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HabilidadBehaviour1 : StateMachineBehaviour

{
    [SerializeField] private GameObject habilidad;
   [SerializeField] private float offsetY;
   private Jefe jefe1;
    private Transform jugador;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
       jefe1 = animator.GetComponent<Jefe>();
       jugador = jefe1.jugador;

        jefe1.MirarJugador();

        Vector2 posicionAparicion = new Vector2(jugador.position.x, jugador.position.y + offsetY);
       Instantiate(habilidad, posicionAparicion, Quaternion.identity);
    }
}
