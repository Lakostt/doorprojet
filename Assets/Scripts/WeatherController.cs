using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeatherController : MonoBehaviour{
	[SerializeField] Material sky;
	[SerializeField] Light sun;
	float FullIntensity;
	float CloudValue = 0;
	public float step = 0.005f;

	void SetClouds(float value)
	{
		sky.SetFloat ("_Blend", value);
		float max = (Managers.Weather.SunRise + Managers.Weather.SunSet) / 2f;
		float cur = DateTime.Now.Hour * 60 + DateTime.Now.Minute;
		if (cur < max) {
			sun.intensity = ((cur - Managers.Weather.SunRise) / (max - Managers.Weather.SunRise)) * FullIntensity;
		}
		else {      
			sun.intensity = ((Managers.Weather.SunSet - cur) / (Managers.Weather.SunSet - max)) * FullIntensity;
		}
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