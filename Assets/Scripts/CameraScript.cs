using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	[SerializeField] Transform target;
	public float rotSpeed = 1.5f;
	float rotY;
	Vector3 offset;

	// Use this for initialization
	void Start () {
		rotY = transform.eulerAngles.y;
		offset = target.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float horInput = Input.GetAxis ("Horizontal");
		if (horInput != 0) {
			rotY += horInput * rotSpeed;
		}
			else
			rotY += Input.GetAxis ("Mouse X") * rotSpeed * 3;
		
		Quaternion rotation = Quaternion.Euler (0, rotY, 0);
		transform.position = target.position - (rotation * offset);
		transform.LookAt (target);
	}
}