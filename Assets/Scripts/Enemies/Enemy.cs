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
	[HideInInspector]
	public GameObject m_player;
	GameController m_gameCtrlScript;
	GameObject m_gameCtrl;

	public float m_wanderRange;
	public float m_damageDistance;


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
			m_gameCtrlScript.EnemyDied();
		}
	}

	void OnParticleCollision(GameObject other) {
		if (other.tag == "BulletTracer") {
			TakeDamage();
		}
	}
}
