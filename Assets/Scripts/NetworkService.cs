using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NetworkService : MonoBehaviour {
	const string xmlApi = "http://api.openweathermap.org/data/2.5/weather?id=524901&mode=xml&appid=8ed04e8d1fdd73ab1ee0e1eda30ee4b4";
	const string JsonApi = "http://api.openweathermap.org/data/2.5/weather?id=524901&appid=8ed04e8d1fdd73ab1ee0e1eda30ee4b4";	
	const string WebImage = "https://images.alphacoders.com/840/840638.png";
	Dictionary<string, string> flag = new Dictionary<string, string>();

		// Россия, США, Англия, Франция, Германия
		
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
	public IEnumerator DownloadImage(Action<Texture2D> callback)
	{
		flag ["RU"] = "http://www.rabstol.net/uploads/gallery/main/229/rabstol_net_flags_70.jpg";
		flag ["US"] = "http://www.rabstol.net/uploads/gallery/main/229/rabstol_net_flags_57.jpg";
		flag ["GB"] = "http://www.rabstol.net/uploads/gallery/main/229/rabstol_net_flags_59.jpg";
		flag ["FR"] = "http://www.rabstol.net/uploads/gallery/main/229/rabstol_net_flags_61.jpg";
		flag ["DE"] = "http://www.rabstol.net/uploads/gallery/main/229/rabstol_net_flags_44.jpg";

		WWW www = new WWW (flag[Managers.Weather.Abbr]);
		yield return www;
		callback (www.texture);
	}
}