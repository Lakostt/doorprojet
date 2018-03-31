using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]

public class Managers : MonoBehaviour {
	
	public static PlayerManager Player { get; private set; }
	public static InventoryManager Inventory { get; private set; }
	public static WeatherManager Weather { get; private set; }

	private List<IGameManager> _managers;

	void Awake () {
		Player = GetComponent<PlayerManager> ();
		Inventory = GetComponent<InventoryManager> ();
		Weather = GetComponent<WeatherManager> ();
		_managers = new List<IGameManager> ();
		_managers.Add (Player);
		_managers.Add (Inventory);
		_managers.Add (Weather);
		StartCoroutine (StartManagers());
	}
	private IEnumerator StartManagers()
	{
		NetworkService network = new NetworkService ();
		foreach (IGameManager manager in _managers) {
			manager.StartUp (network);
		}
		yield return null;
		int numModules = _managers.Count;
		int numReady = 0;
		while (numReady < numModules) {
			int lastReady = numReady;
			numReady = 0;
			foreach (IGameManager manager in _managers) {
				if (manager.status == ManagerStatus.Started)
					numReady++;
				}
			if(numReady > lastReady)
			{
				Debug.Log ("Progress: " + numReady + "/" + numModules);
			}
			yield return null;
		}
		Debug.Log ("All Managers startup..");
	}
	// Every frame
	void Update () {
		
	}
}
