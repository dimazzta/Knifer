using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TriggerHandler : MonoBehaviour {
	public AudioSource source;
	public GameObject sliver, bloodSplatter, wound;	
	public GameContoller gameController;
	private ParticleSystem instance;
	public AudioClip[] clips;
	public SliverPool pool;
	public Transform bladePosition;
	public Text text;
	private bool gameLost = false;
	
	IEnumerator GameOverRoutine(int score = 0, float delay = 2.0f){
		yield return new WaitForSeconds(delay);
		gameController.GameOver(score);
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Hand"){
			// gameLost = true;
			source.clip = clips[Random.Range(0, clips.Length)];
			source.Play();
			RaycastHit hit;
			if (Physics.Raycast(new Ray(bladePosition.position, Vector3.down), out hit)){
				instance = Instantiate(bloodSplatter, hit.point, Quaternion.identity).GetComponentInChildren<ParticleSystem>();
			}
			int score = int.Parse(text.text);
			// StartCoroutine(GameOverRoutine(score, 0.15f));
			
		}
		else {
			if (!gameLost){
				RaycastHit hit;	
				if (Physics.Raycast(new Ray(bladePosition.position, Vector3.down), out hit)){
					pool.instObject(new Vector3(hit.point.x, 2.5f, hit.point.z), 
					transform.parent.rotation);
				}
				int score = int.Parse(text.text);
				score++;
				text.text = score.ToString();
			}
		}
	}
	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if (instance){
			if (!instance.IsAlive()){
				Destroy(instance.gameObject);
			}
		}

	}

	// void OnCollisionEnter(Collision other)
	// {
	// 	if (isColliding) return;
	// 	isColliding = true;
	// 	if (other.transform.tag == "Hand"){
	// 		source.clip = clips[Random.Range(0, clips.Length)];
	// 		source.Play();
	//  	}
	// 	else {
	// 		pool.instObject(new Vector3(other.contacts[0].point.x, 2.5f, other.contacts[0].point.z), 
	// 	transform.parent.rotation);
	// 	}
		
	// }

}
