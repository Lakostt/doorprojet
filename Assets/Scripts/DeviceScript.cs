using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceScript : MonoBehaviour {
	public float radius = 1.5f; // радиус невидимой сферы
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E)) {
			Collider[] colls = Physics.OverlapSphere (
				transform.position, radius);
			foreach (Collider coll in colls) {
				Vector3 direction = coll.transform.position -
				                    transform.position;
				if (Vector3.Dot(transform.forward, direction) > .5f)
				coll.SendMessage ("Operate",
					SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
