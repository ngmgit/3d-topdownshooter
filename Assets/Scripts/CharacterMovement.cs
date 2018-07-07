using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

	Rigidbody rb;
	public float speed =  4;

	private Vector3 lookPos;
	InputController m_input;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		m_input = GetComponent<InputController>();
	}

	void Update ()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100)) {
			lookPos = hit.point;
		}

		Vector3 lookDir = lookPos - transform.position;
		lookDir.y = 0;

		transform.LookAt (transform.position + lookDir, Vector3.up);
	}

	void FixedUpdate ()
	{
		Vector3 movement  =  new Vector3(m_input.m_horizontal, 0, m_input.m_vertical);
		rb.AddForce(movement * speed / Time.deltaTime);
	}
}
