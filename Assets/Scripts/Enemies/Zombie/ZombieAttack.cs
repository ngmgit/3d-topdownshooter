﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : ZombieFSMBase {

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter (animator, stateInfo, layerIndex);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (!m_enemyScript.m_isDead) {
			if (Vector3.Distance(m_player.transform.position, NPC.transform.position) > Enemy.ATTACK_DISTANCE) {
				animator.SetBool ("Attack", false);
			}

			Vector3 relativePos = m_player.transform.position - NPC.transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			NPC.transform.rotation = rotation;
		}
	}

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
