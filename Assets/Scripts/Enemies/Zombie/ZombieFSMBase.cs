using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieFSMBase : StateMachineBehaviour {

	protected GameObject NPC;
	protected NavMeshAgent m_agent;
	protected Enemy m_enemyScript;
	protected GameObject m_player;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		NPC = animator.gameObject;
		m_enemyScript = NPC.GetComponent <Enemy>();
		m_agent = NPC.GetComponent <NavMeshAgent>();
		m_player = m_enemyScript.m_player;
	}
}
