using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe_CaminarBehaviour1 : StateMachineBehaviour
{
    private Jefe1 jefe1;
    private Rigidbody2D rb2D;

   [SerializeField] private float velocidadMovimiento;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        jefe1 = animator.GetComponent<Jefe1>();
        rb2D = jefe1.rb2D;
        jefe1.MirarJugador();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
       rb2D.linearVelocity = new Vector2(velocidadMovimiento, rb2D.linearVelocity.y) * animator.transform.right;
   }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      rb2D.linearVelocity = new Vector2(0, rb2D.linearVelocity.y);
   }
}
