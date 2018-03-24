using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour, IGameManager {
	public Managers status { get ; private set; }
	NetworkService network;
	public void StartUp(NetworkService service)
	{
		Debug.Log ("Weather Managers starting...");
		network = service;
		StartCoroutine (network.GetXML(OnXMLData));
		status = ManagerStatus.Started;
	}
	public void OnXMLData(string data)
	{
		Debug.Log (data);
		status = ManagerStatus.Started;
	}
}	