using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public List<Transform> m_spawnPoints;
	public GameObject m_zombie;

	// Use this for initialization
	void Start () {
		foreach (Transform spawn in m_spawnPoints) {
			for (int i = 0; i< 10 ; i++) {
				Instantiate(m_zombie, spawn);
			}
		}
	}
}
