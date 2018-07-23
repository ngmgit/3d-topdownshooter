using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour {

	public float m_speed =  1.3f;
	public Text m_healthText;
	public ParticleSystem m_bulletParticleSystem;

	Rigidbody rb;
	float m_health = 150f;
	Vector3 lookPos;
	InputController m_input;
	ParticleSystem.EmissionModule m_emissionModule;


	// Use this for initialization
	void Start ()
	{
		m_healthText.text = "HEALTH: " + m_health;
		rb = GetComponent<Rigidbody>();
		m_input = GetComponent<InputController>();
		m_emissionModule = m_bulletParticleSystem.emission;
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

		m_emissionModule.enabled = m_input.m_shoot;
	}

	void FixedUpdate ()
	{
		Vector3 movement  =  new Vector3(m_input.m_horizontal, 0, m_input.m_vertical);
		rb.AddForce(movement.normalized * m_speed / Time.deltaTime);
	}

	public void DamagePlayer (float dmg) {
		m_health = m_health - dmg;
		SetHealthText ();
	}

	void SetHealthText () {
		m_healthText.text = "HEALTH: " + m_health;
	}
}
