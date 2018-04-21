using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerScript : MonoBehaviour {

	public void Operate()
	{
		Managers.Images.GetWebImage (OnWebImage);
	}

	void OnWebImage(Texture2D image)
	{
		GetComponent<Renderer> ().material.mainTexture = image;
	}
}
