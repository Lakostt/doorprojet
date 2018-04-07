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
		if (Managers.Weather.tCur < Managers.Weather.tMax) {
			sun.intensity = ((Managers.Weather.tCur -= Managers.Weather.SunRise) / (Managers.Weather.tMax - Managers.Weather.tMax)) * FullIntensity;
		}
		if (Managers.Weather.tCur > Managers.Weather.tMax) {
			sun.intensity = ((Managers.Weather.SunSet -= Managers.Weather.tCur) / (Managers.Weather.SunSet -= Managers.Weather.tMax)) * FullIntensity;
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