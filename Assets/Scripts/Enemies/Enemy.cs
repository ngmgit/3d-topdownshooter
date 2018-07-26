using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
	public const float WALK_SPEED = 1.7f;
	public const float CHASE_SPEED = 8f;
	public const float CHASE_INITIATE_DISTANCE = 15f;
	public const float CHASE_THRESHOLD_DISTANCE = 20f;
	public const float ATTACK_DISTANCE = 2f;
	public const float DAMAGE = 5f;

	const float HEALTH = 100;
	const float DAMAGE_ONHIT = 20;

	float currentHealth;
	Animator m_animator;
	GameController m_gameCtrlScript;
	GameObject m_gameCtrl;
	NavMeshAgent m_agent;

	[HideInInspector]
	public GameObject m_player;
	public float m_wanderRange;
	public float m_damageDistance;
	[HideInInspector]
	public bool m_isDead = false;


	public Vector3 GetRandomNavMeshPositin () {
		Vector3 randomPoint = transform.position + Random.insideUnitSphere * m_wanderRange;
		NavMeshHit navHit;
		NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, NavMesh.AllAreas);
		return navHit.position;
	}

	void Awake () {
		m_player = GameObject.FindGameObjectWithTag ("Player");
		m_gameCtrl = GameObject.FindGameObjectWithTag ("GameController");
		m_gameCtrlScript = m_gameCtrl.GetComponent <GameController> ();
		m_agent = GetComponent <NavMeshAgent> ();
		currentHealth = HEALTH;

		if (GetComponent <Animator> ()) {
			m_animator = GetComponent <Animator> ();
		} else {
			m_animator = GetComponentInChildren <Animator> ();
		}
	}

	void Start () {
		m_animator.SetBool("Walk", true);
	}

	void Update () {
		if (transform.position.y < -1) {
			Destroy (gameObject);
		}
	}
	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player") {
			m_animator.SetBool ("Attack", true);
		}
	}

	void OnTriggerEnterStay (Collider col) {
		if (col.tag == "Player") {
			m_animator.SetBool ("Attack", true);
		}
	}

	void DamagePlayer () {
		if (Vector3.Distance(m_player.transform.position, transform.position) < m_damageDistance) {
			m_player.GetComponent<CharacterMovement>().DamagePlayer(Enemy.DAMAGE);
		}
	}

	void TakeDamage () {
		currentHealth -= DAMAGE_ONHIT;
		if (currentHealth <= 0) {
			m_animator.SetBool ("Dead", true);
		}
	}

	// Detect tracer particle to damge the zombie
	void OnParticleCollision(GameObject other) {
		if (other.tag == "BulletTracer") {
			TakeDamage();
		}
	}


	// Used to disable neccessary component when zombie dies
	// NOTE:
	// Using Animation events to trigger the method
	void SetDeadState () {
		m_isDead = true;
		m_gameCtrlScript.EnemyDied();

		m_agent.velocity = Vector3.zero;
		m_agent.isStopped = true;
		m_agent.enabled = false;

		GetComponent<CapsuleCollider>().enabled = false;
		GetComponent<SphereCollider>().enabled = false;
	}
}
