using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {
	[SerializeField] string ItemName;
	void OnTriggerEnter(Collider Coll)
	{
		Managers.Inventory.addItem (ItemName);
		Destroy (gameObject);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
