using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SliverPool : MonoBehaviour {

	public GameObject prefab;
	public int maxSize = 1000;
	private Stack<GameObject> inactiveInstances = new Stack<GameObject>();
	private Queue<GameObject> activeInstances = new Queue<GameObject>();
	public void instObject(Vector3 position, Quaternion rotation){
		if (activeInstances.Count > maxSize){
			Sequence s = DOTween.Sequence();
			GameObject go = activeInstances.Dequeue();
			go.SetActive(false);
			inactiveInstances.Push(go);
		}
		if (inactiveInstances.Count > 0) {
				GameObject newGO = inactiveInstances.Pop();
				newGO.transform.position = position;
				newGO.transform.rotation = rotation;
				newGO.SetActive(true);
				newGO.GetComponent<Renderer>().material.DOFade(1, 0);
				activeInstances.Enqueue(newGO);
			}
			else {
				activeInstances.Enqueue(Instantiate(prefab, position, rotation));
			}
		}
	

}
