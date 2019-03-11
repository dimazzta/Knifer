using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapHandler : MonoBehaviour {
	public KnifeMover km;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0)){
			km.Hit();
		}
	}
}
