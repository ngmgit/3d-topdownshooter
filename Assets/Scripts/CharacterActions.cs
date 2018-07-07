using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions : MonoBehaviour {

	Animator m_animator;
	InputController m_input;
	Animation m_animation;

	// Use this for initialization
	void Awake () {
		m_animator = GetComponent <Animator> ();
		m_input = GetComponent<InputController>();
		m_animator.SetTrigger ("isNotIdle");
	}

	// Update is called once per frame
	void Update () {
		float m_moving = Mathf.Abs(m_input.m_horizontal)  + Mathf.Abs(m_input.m_vertical) / 2;
		m_animator.SetFloat("moving", m_moving);
		m_animator.SetBool("isShooting", m_input.m_shoot);
	}
}
