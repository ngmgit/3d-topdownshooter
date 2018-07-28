using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	/*
	For isometric camera set up
	offsets = Vector3 (9, 12, -10)
	Rotation = Vector3 ( 40, -45, 0)

	 */
	public Transform m_target;
	public Vector3 m_cameraOffsets;

	// Update is called once per frame
	void LateUpdate () {
		transform.position = m_target.position + m_cameraOffsets;
	}
}
