using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AnimState_PC_Falling : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.parent.GetComponent<Pawn>().Move();

        BoxCollider bc = animator.transform.parent.GetComponent<BoxCollider>();
        RaycastHit[] hits = Physics.BoxCastAll(
            animator.transform.position + bc.center,
            bc.size / 2,
            -animator.transform.up,
            animator.transform.rotation,
            0.01f,
            0b010000000
        );

        if (hits.Length > 0)
        {
            GameObject gameObj_ground = hits.OrderBy(hit => hit.distance).First().collider.gameObject;
            animator.SetBool("OnGround", true);
        }
        else
        {
            animator.SetBool("OnGround", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
