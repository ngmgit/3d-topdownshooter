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
	Rigidbody rb;
	GameObject[] m_bloodSpawnPoints;

	[HideInInspector]
	public GameObject m_player;
	[HideInInspector]
	public bool m_isDead = false;

	public float m_wanderRange;
	public float m_damageDistance;
	public GameObject m_BloodSpawn;
	public GameObject m_bloodParticleSystem;


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
		rb = GetComponent <Rigidbody> ();
		currentHealth = HEALTH;


		if (GetComponent <Animator> ()) {
			m_animator = GetComponent <Animator> ();
		} else {
			m_animator = GetComponentInChildren <Animator> ();
		}

	}

	void Start () {
		int spawnCount = 0;
		m_bloodSpawnPoints= new GameObject[m_BloodSpawn.transform.childCount];
		foreach (Transform t in m_BloodSpawn.transform) {
			m_bloodSpawnPoints [ spawnCount ] = t.gameObject;
			spawnCount++;
		}
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
		PlayBloodParticles ();
		if (currentHealth <= 0) {
			m_animator.SetBool ("Dead", true);
		}
	}

	void PlayBloodParticles () {
		int m_randSpawn = Random.Range(0,3);
		GameObject blood = Instantiate(m_bloodParticleSystem, m_bloodSpawnPoints[m_randSpawn].transform) as GameObject;
		blood.transform.position = m_bloodSpawnPoints[m_randSpawn].transform.position;
		blood.transform.parent = m_bloodSpawnPoints[m_randSpawn].transform;
	}

	// Detect tracer particle to damge the zombie
	void OnParticleCollision(GameObject other) {
		if (other.tag == "BulletTracer") {
			if (!m_isDead) {
				TakeDamage();
			}
		}
	}


	// Used to disable neccessary component when npc dies
	// NOTE:
	// Using Animation events to trigger the method
	void SetDeadState () {
		m_isDead = true;
		m_gameCtrlScript.EnemyDied();

		rb.velocity = Vector3.zero;

		m_agent.velocity = Vector3.zero;
		m_agent.isStopped = true;
		m_agent.enabled = false;

		GetComponent<CapsuleCollider>().enabled = false;
		GetComponent<SphereCollider>().enabled = false;
	}

	// Used to stop the navagent when npc dies
	// NOTE:
	// Using Animation events to trigger the method
	void StopNavAgent () {
		m_agent.destination = transform.position;
		m_agent.acceleration = 0;
	}
}
