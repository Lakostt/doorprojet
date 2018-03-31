using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;

public class WeatherManager : MonoBehaviour, IGameManager {
	public ManagerStatus status { get ; private set; }
	public float CloudValue { get; private set; }
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
		XmlDocument doc = new XmlDocument ();
		doc.LoadXml (data);
		XmlNode root = doc.DocumentElement;
		XmlNode node = root.SelectSingleNode ("clouds");
		string clouds = node.Attributes ["value"].Value;
		CloudValue = int.Parse (clouds) / 100f;
		Messenger<float>.Broadcast (GameEvent.WEATHER_UPDATED,CloudValue);
		status = ManagerStatus.Started;
	}
}	