using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
	public const float WALK_SPEED = 1.7f;
	public const float CHASE_SPEED = 6f;
	public const float CHASE_INITIATE_DISTANCE = 13f;
	public const float CHASE_THRESHOLD_DISTANCE = 20f;
	public const float ATTACK_DISTANCE = 2f;
	public const float DAMAGE = 10f;

	NavMeshAgent m_agent;
	Animator m_animator;
	[HideInInspector]
	public GameObject m_player;

	public float m_range;
	public float m_damageDistance;


	public Vector3 GetRandomNavMeshPositin () {
		Vector3 randomPoint = transform.position + Random.insideUnitSphere * m_range;
		NavMeshHit navHit;
		NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, NavMesh.AllAreas);
		return navHit.position;
	}

	void Awake () {
		m_player = GameObject.FindGameObjectWithTag ("Player");
		m_agent = GetComponent <NavMeshAgent> ();

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
	}

	void OnTriggerStay (Collider col) {
		if (col.tag == "Player") {
			m_animator.SetBool ("Attack", true);
		}
	}

	void DamagePlayer () {
		if (Vector3.Distance(m_player.transform.position, transform.position) < m_damageDistance) {
			m_player.GetComponent<CharacterMovement>().DamagePlayer(Enemy.DAMAGE);
		}
	}
}
