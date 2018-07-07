using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	[HideInInspector] public float m_vertical;
	[HideInInspector] public float m_horizontal;
	[HideInInspector] public bool m_shoot;

	// Update is called once per frame
	void Update () {
		m_horizontal = Input.GetAxis ("Horizontal");
		m_vertical = Input.GetAxis ("Vertical");
		m_shoot = Input.GetKeyDown (KeyCode.Mouse0);
	}
}
