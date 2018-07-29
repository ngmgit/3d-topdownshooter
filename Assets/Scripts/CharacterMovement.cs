using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour {

	public float m_speed =  1.3f;
	public Text m_healthText;
	public GameObject m_tracerGameObject;
	public ParticleSystem m_gunParticleSystem;
	public Image m_currentHeathImage;

	const float MAX_HEALTH = 150f;
	Rigidbody rb;
	float m_health = MAX_HEALTH;
	Vector3 lookPos;
	InputController m_input;
	ParticleSystem m_TracerParticleSystem;
	float m_bulletDistance = 0;


	// Use this for initialization
	void Start ()
	{
		m_TracerParticleSystem = m_tracerGameObject.GetComponent <ParticleSystem> ();
		m_healthText.text = "HEALTH: " + m_health;
		rb = GetComponent<Rigidbody>();
		m_input = GetComponent<InputController>();
	}

	void Update ()
	{
		RotatePlayerAlongMousePosition ();

		if (m_bulletDistance == 0) {
			SetBulletDistance ();
		}

		m_tracerGameObject.SetActive (m_input.m_shoot);
	}

	void FixedUpdate ()
	{

		if (m_input.m_shoot) {
			m_gunParticleSystem.Emit(1);
		}

		Vector3 movement  =  new Vector3(m_input.m_horizontal, 0, m_input.m_vertical);
		rb.AddForce(movement.normalized * m_speed);
	}

	void RotatePlayerAlongMousePosition () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100)) {
			lookPos = hit.point;
		}

		Vector3 lookDir = lookPos - transform.position;
		lookDir.y = 0;

		transform.LookAt (transform.position + lookDir, Vector3.up);
	}

	void SetBulletDistance () {
		Vector3 lookPos;

		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0, 0.8f,0));
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100)) {
			lookPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
			m_bulletDistance = Vector3.Distance (transform.position, lookPos);
			ParticleSystem.MainModule m_main = m_TracerParticleSystem.main;
			m_main.startLifetime = ( m_bulletDistance / m_main.startSpeed.constant ) / 1.5f ;
		}
	}

	public void DamagePlayer (float dmg) {
		m_health = m_health - dmg;
		if (m_health >= 0) {
			SetHealthText ();
		}

	}

	void SetHealthText () {
		float m_healthRatio = m_health / MAX_HEALTH;

		m_currentHeathImage.rectTransform.localScale = new Vector3 (m_healthRatio, 1, 1);
		m_healthText.text = "HEALTH: " + m_health;
	}
}
