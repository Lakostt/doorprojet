using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour{
	[SerializeField] Material sky;
	[SerializeField] Light sun;
	float FullIntensity;
	float CloudValue = 0;
	public float step = 0.005f;

	void SetClouds(float value)
	{
		sky.SetFloat ("_Blend", value);
		sun.intensity = (FullIntensity * value);
	}

	void Awake()
	{
		Messenger<float>.AddListener (GameEvent.WEATHER_UPDATED,SetClouds);
	}

	void Start () {
		FullIntensity = sun.intensity;
			
	}

	void Update () {


		}
	}