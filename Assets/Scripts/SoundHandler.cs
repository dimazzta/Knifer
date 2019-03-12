using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundHandler : MonoBehaviour {

	public Texture2D imageOn;
	public Texture2D imageOff;
	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		if (PlayerPrefs.GetInt("audio") == 0){
			AudioListener.volume = 1f;
			GetComponentInChildren<RawImage>().texture = imageOn;
		}
		else{
			AudioListener.volume = 0f;
			GetComponentInChildren<RawImage>().texture = imageOff;
		}
	}
	public void TurnOff(){
		GetComponentInChildren<RawImage>().texture = imageOff;
		AudioListener.volume = 0f;
	}
	public void TurnOn(){
		GetComponentInChildren<RawImage>().texture = imageOn;
		AudioListener.volume = 1f;
	}

	public void Toggle(){
		if (PlayerPrefs.GetInt("audio") == 0){
			TurnOff();
			PlayerPrefs.SetInt("audio", 1);
		}
		else{
			TurnOn();
			PlayerPrefs.SetInt("audio", 0);
		}
	}
}
