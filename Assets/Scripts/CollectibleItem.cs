using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {
	[SerializeField] string ItemName;
	void OnTriggerEnter(Collider Coll)
	{
		Debug.Log ("Item collecteed: " + ItemName);
		Destroy (gameObject);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
