using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	public GameObject m_zombie;
	public int m_totalEnemies;
	public Text m_deathContText;

	int m_deathCount;

	// Use this for initialization
	void Start () {
		m_totalEnemies = transform.childCount * 4;
		foreach (Transform spawnObject in transform) {
			for (int i = 0; i< 4 ; i++) {
				Instantiate(m_zombie, spawnObject.transform);
			}
		}
	}

	public void EnemyDied () {
		m_deathCount += 1;
		SetDeathCountText ();
	}

	void SetDeathCountText () {
		m_deathContText.text = "Enemies Dead: " + m_deathCount.ToString() + "/" + m_totalEnemies.ToString();
	}
}
