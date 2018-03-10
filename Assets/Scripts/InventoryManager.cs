using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager {
	public ManagerStatus status { get; private set; }

	public void StartUp()
	{
		Debug.Log ("Inventory Manager starting..");
		status = ManagerStatus.Started;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
