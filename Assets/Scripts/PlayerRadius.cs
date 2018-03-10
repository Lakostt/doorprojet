using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRadius : MonoBehaviour {
	public float radius = 1.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			Collider[] Colliders = Physics.OverlapSphere(transform.position,radius);
			foreach (Collider Coll in Colliders) {
				Vector3 direction = Coll.transform.position - transform.position;
				if (Vector3.Dot (transform.forward, direction) > 0.5f) {
					Coll.SendMessage ("Operate", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}