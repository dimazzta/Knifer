using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundController : MonoBehaviour {
	public static AmbientSoundController instance;
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
	}
	public void TurnOn(){
		GetComponent<AudioSource>().Play();
	}
	public void TurnOff(){
		GetComponent<AudioSource>().Pause();
	}
}
