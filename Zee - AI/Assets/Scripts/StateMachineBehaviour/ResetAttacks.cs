using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAttacks : StateMachineBehaviour {


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        #region Reset All Attacks

        animator.gameObject.GetComponent<InputManager>().noOfClicks = 0;
        animator.gameObject.GetComponent<InputManager>().canChangeMaxInput = true;

        animator.SetBool("IsFalling", false);
        animator.ResetTrigger("Simple1");
        animator.ResetTrigger("Simple2");
        animator.ResetTrigger("Simple3");
        animator.ResetTrigger("Simple4");
        animator.ResetTrigger("Down2");
        animator.ResetTrigger("Down2");
        animator.ResetTrigger("Up1");
        animator.ResetTrigger("Up2");
        animator.ResetTrigger("Up3");
        animator.ResetTrigger("SimpleAir1");
        animator.ResetTrigger("SimpleAir2");
        animator.ResetTrigger("SimpleAir3");

        #endregion
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
