using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using MiniJSON;

public class WeatherManager : MonoBehaviour, IGameManager {
	public ManagerStatus status { get ; private set; }
	public float CloudValue { get; private set; }
	public float SunRise;
	public float SunSet;
	public string Abbr { get; private set; }
	NetworkService network;
	public void StartUp(NetworkService service)
	{
		Debug.Log ("Weather Managers starting...");
		network = service;
		StartCoroutine (network.GetJSON(OnJSONData));
		status = ManagerStatus.Started;
	}
	public void OnXMLData(string data)
	{
		XmlDocument doc = new XmlDocument ();
		doc.LoadXml (data);
		XmlNode root = doc.DocumentElement;
		XmlNode node = root.SelectSingleNode ("clouds");
		XmlNode city = root.SelectSingleNode ("city");
		XmlNode sun = city.SelectSingleNode ("sun");
		string rise = sun.Attributes ["rise"].Value;
		string rset = sun.Attributes ["set"].Value;
		int rrise = int.Parse(rise.Substring (11,2));
		int rrset = int.Parse(rset.Substring (11,2));
		rrise += 3;
		rrset += 3;
		// Public
		SunRise = rrise;
		SunSet = rrset;

		string clouds = node.Attributes ["value"].Value;
		CloudValue = int.Parse (clouds) / 100f;
		Messenger<float> .Broadcast (GameEvent.WEATHER_UPDATED,CloudValue);
	
		status = ManagerStatus.Started;
	}

	public void OnJSONData(string data)

	{
		Dictionary<string,object> dict;
		dict = Json.Deserialize(data) as Dictionary<string,object>;	
		Dictionary<string,object> clouds = (Dictionary<string,object>) dict["clouds"];
		CloudValue = (long)clouds ["all"] / 100f;
		Debug.Log (CloudValue);

		Dictionary<string,object> sys = (Dictionary<string,object>) dict["sys"];
		double SunRiseD = (long)sys ["sunrise"];
		double SunSetD = (long)sys ["sunset"];

		Abbr = (string)sys ["country"];
		Debug.Log (Abbr);

		DateTime timeRise = new DateTime (1970, 1, 1, 0, 0, 0);
		timeRise = timeRise.AddSeconds(SunRiseD);
		timeRise = timeRise.AddHours (3);
		DateTime timeSet = new DateTime(1970, 1, 1, 0, 0, 0);
		timeSet = timeSet.AddSeconds (SunSetD);
		timeSet = timeSet.AddHours (3);

		SunRise = timeRise.Hour * 60 + timeRise.Minute;
		SunSet = timeSet.Hour * 60 + timeSet.Minute;

		Messenger<float> .Broadcast (GameEvent.WEATHER_UPDATED,CloudValue);
		status = ManagerStatus.Started;
	}
}	