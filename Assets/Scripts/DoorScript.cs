using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {
	[SerializeField] private Vector3 dPos;	
	bool open;
	public void Operate()
	{
		if (open) {
			transform.position -= dPos;
		} else {
			transform.position += dPos;
		}
		open = !open;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}