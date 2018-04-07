using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;

public class WeatherManager : MonoBehaviour, IGameManager {
	public ManagerStatus status { get ; private set; }
	public float CloudValue { get; private set; }
	public int SunRise { get; private set; }
	public int SunSet { get; private set; }
	public float tMax {get; private set; }
	public int tCur { get; private set; }
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
		XmlNode city = root.SelectSingleNode ("city");
		XmlNode sun = city.SelectSingleNode ("sun");
		string rise = sun.Attributes ["rise"].Value;
		string rset = sun.Attributes ["set"].Value;
		int rrise = int.Parse(rise.Substring (11,2));
		int rrset = int.Parse(rset.Substring (11,2));
		rrise += 3;
		rrset += 3;
		int tcur = DateTime.Now.Hour * 60;
		tcur += DateTime.Now.Minute;
		int tmax = (rrset - rrise) / 2;
		// Public
		SunRise = rrise;
		SunSet = rrset;
		tMax = tmax;
		tCur = tcur;
		Debug.Log (rrise);
		Debug.Log (rrset);
		Debug.Log (tcur);
		string clouds = node.Attributes ["value"].Value;
		CloudValue = int.Parse (clouds) / 100f;
		Messenger<float> .Broadcast (GameEvent.WEATHER_UPDATED,CloudValue);
	

		status = ManagerStatus.Started;
	}
}	