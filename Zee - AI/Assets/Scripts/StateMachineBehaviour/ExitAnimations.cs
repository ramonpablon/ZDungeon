using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitAnimations : StateMachineBehaviour {

    public int maxComboNum = 0;
    public string comboName;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	 //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (animator.GetBool(comboName + animator.gameObject.GetComponent<InputManager>().noOfClicks.ToString()) == true)
        {
            animator.SetBool("Exit", false);
        }
        else
            animator.SetBool("Exit", true);
	}

	 //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (animator.gameObject.GetComponent<InputManager>().noOfClicks == maxComboNum)
            animator.gameObject.GetComponent<InputManager>().noOfClicks = 0;
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
