using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	Transform m_waypoint;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		m_waypoint = GameObject.Find("Waypoint").transform;
		agent = GetComponent <NavMeshAgent> ();
		if (!m_waypoint) {
			Debug.Log ("No waypt found!!");
		} else {
			agent.destination = m_waypoint.position;
		}
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftAlt)) {
			agent.destination = m_waypoint.position;
			Debug.Log (m_waypoint.position);
		}
	}
}
