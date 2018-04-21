using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MiniJSON;

public class ImagesManager : MonoBehaviour, IGameManager {

	public ManagerStatus status { get; private set; }
	NetworkService network;
	Texture2D WebImage;


	public void StartUp(NetworkService service)
	{
		Debug.Log ("Image manager started..");
		network = service;
		status = ManagerStatus.Started;
	}

	public void GetWebImage (Action<Texture2D> callback)
	{
		if (WebImage == null) {
			StartCoroutine (network.DownloadImage ((Texture2D image) =>
				{
					WebImage = image;
					callback(WebImage);
				}
			
			));
		} else {
			callback (WebImage);
		}
	
	}
}
