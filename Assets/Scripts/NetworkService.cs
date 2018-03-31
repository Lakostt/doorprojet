using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetworkService : MonoBehaviour {
	const string xmlApi = "http://samples.openweathermap.org/data/2.5/weather?q=London&mode=xml&appid=b6907d289e10d714a6e88b30761fae22";
	bool IsResponce(WWW www)
	{
		if (www.error != null) {
			Debug.Log ("Bad connection");
			return false;
		} else if (string.IsNullOrEmpty (www.text)) {
			Debug.Log ("Bad data");
			return false;
		} else
			return true;
	}
	IEnumerator CallAPI(string url,Action<string> callback) {
		WWW www = new WWW (url);
		yield return www;
		if(!IsResponce(www))
		{
			yield break;
		}
		callback (www.text);
	}

	public IEnumerator GetXML(Action<string> callback)
	{
		return CallAPI (xmlApi, callback);
	}
}