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

	// Use this for initialization
	void Start () {
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
		
		m = transform.DOPath(path, 5f / Speed, PathType.CatmullRom, PathMode.Ignore, 5);
		m.SetLoops(-1, LoopType.Yoyo);
		m.SetEase(Ease.Linear);
		s = transform.DORotate(new Vector3(transform.rotation.x, 
		-40, transform.rotation.z), 5f / Speed).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
		StartCoroutine(increaseSpeed());
	
	}

    private IEnumerator increaseSpeed()
    {
		while (true) {
			Speed += speedIncreasePerSecond;
			yield return new WaitForSeconds(1f);
		}
		
    }



    // Update is called once per frame
    void Update () {
		
		m.timeScale = Speed;
		s.timeScale = Speed;

		// RaycastHit hit;
		//  if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        // {
        //     Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        //     Debug.Log(hit.transform.tag);
        // }
        // else
        // {
        //     Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
        //     Debug.Log("Did not Hit");
        // }
	}


	public void Hit(){
		m.Pause();
		s.Pause();
		Sequence seq = DOTween.Sequence();
		Tween t = transform.DOMoveY(3.3f, .1f).SetEase(Ease.OutElastic).SetLoops(1, LoopType.Yoyo);
		GetComponent<AudioSource>().Play();
		seq.Append(t);
		seq.AppendCallback(() => {
			m.Play();
			s.Play();
		});
	}


}
