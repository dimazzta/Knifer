using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class KnifeMover : MonoBehaviour {
	Vector3[] path;
	Vector3[] inversePath;
	Vector3[] resultPath;
	public Transform[] stabs;

	[Range(1f, 10f)]
	public float Speed = 1f;
	public float speedIncreasePerSecond = 0.1f;
	Tween m, s;
	public static KnifeMover instance;
	private AudioSource _source;
	private bool gameStarted;
	private Vector3 initialPosition;
	private Quaternion initialRotation;
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		gameStarted = false;
		_source = GetComponent<AudioSource>();
		initialPosition = transform.position;
		initialRotation = transform.rotation;
	}
	public void SetupMovement(){
		transform.position = initialPosition;
		transform.rotation = initialRotation;
		int i = 0, j = stabs.Length - 1;
		path = new Vector3[stabs.Length];
		inversePath = new Vector3[stabs.Length];
		foreach(Transform t in stabs){
			path[i++] = t.position;
			inversePath[j--] = t.position;
		}
		resultPath = new Vector3[path.Length * 2];
		for(i = 0; i < path.Length * 2; i++){
			resultPath[i] = i < path.Length ? path[i] : inversePath[i - path.Length];
		}
		StopAllCoroutines();
		Speed = 1f;
	}

	public void StartMovement(){
		m = transform.DOPath(path, 5f / Speed, PathType.CatmullRom, PathMode.Ignore, 5);
		m.SetLoops(-1, LoopType.Yoyo);
		m.SetEase(Ease.Linear);
		s = transform.DORotate(new Vector3(transform.rotation.x, 
		-40, transform.rotation.z), 5f / Speed).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
		StartCoroutine(increaseSpeed());
		gameStarted = true;
	}
    
	public int GetCurrentLoop(){
		return m.CompletedLoops();
	}
	public void StopAnimation(){
		m.Kill();
		s.Kill();
		gameStarted = false;
	}
    void Update () {
		if (gameStarted){
			m.timeScale = Speed;
			s.timeScale = Speed;
		}
	}

	public void Hit(){
		if (gameStarted){
			m.Pause();
			s.Pause();
			Sequence seq = DOTween.Sequence();
			Tween t = transform.DOMoveY(3.3f, .1f).SetEase(Ease.OutElastic).SetLoops(1, LoopType.Yoyo);
			_source.Play();
			seq.Append(t);
			seq.AppendCallback(() => {
				m.Play();
				s.Play();
			});
		}
	}

	private IEnumerator increaseSpeed()
    {
		while (true) {
			Speed += speedIncreasePerSecond;
			yield return new WaitForSeconds(1f);
		}
    }

}
