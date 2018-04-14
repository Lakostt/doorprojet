using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetworkService : MonoBehaviour {
	const string xmlApi = "http://api.openweathermap.org/data/2.5/weather?id=524901&mode=xml&appid=8ed04e8d1fdd73ab1ee0e1eda30ee4b4";
	const string JsonApi = "http://api.openweathermap.org/data/2.5/weather?id=524901&appid=8ed04e8d1fdd73ab1ee0e1eda30ee4b4";	
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

	public IEnumerator GetJSON(Action<string> callback)
	{
		return CallAPI (JsonApi, callback);
	}
}