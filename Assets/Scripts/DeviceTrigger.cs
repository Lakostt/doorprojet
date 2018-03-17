using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour {
	[SerializeField] GameObject door;
	public bool RequireKey;
	void OnTriggerEnter(Collider coll)
	{
		if (RequireKey && Managers.Inventory.EquippedItem != "Key") {
			return;
		}
		door.SendMessage ("Operate",SendMessageOptions.DontRequireReceiver);
	}
	void OnTriggerExit(Collider coll)
	{
		door.SendMessage ("Operate", SendMessageOptions.DontRequireReceiver);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
