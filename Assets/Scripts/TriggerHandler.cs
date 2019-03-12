using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class HitInfo{
	public Transform area;
	public int loop;
}
public class TriggerHandler : MonoBehaviour {
	public AudioSource source;
	public GameObject bloodSplatter;	
	private ParticleSystem p_instance;
	public AudioClip[] clips;
	public SliverPool pool;
	public Transform bladePosition;
	private bool gameLost = false;
	private HitInfo previousHit;
	
	public static TriggerHandler instance;

	public void ResetPreviousHit(){
		previousHit = null;
	}
	void Awake()
	{
		previousHit = null;
		if (instance == null){
			instance = this;
		}
		else if (instance != null) {
			Destroy(gameObject);
		}
	}
	IEnumerator GameOverRoutine(int score = 0, float delay = 2.0f){
		yield return new WaitForSeconds(delay);
		GameContoller.instance.GameOver(score);
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Hand"){
			GameContoller.instance.state = GameState.Lost;
			KnifeMover.instance.StopAnimation();
			source.clip = clips[Random.Range(0, clips.Length)];
			source.Play();
			RaycastHit hit;
			if (Physics.Raycast(new Ray(bladePosition.position, Vector3.down), out hit)){
				p_instance = Instantiate(bloodSplatter, hit.point, Quaternion.identity).GetComponentInChildren<ParticleSystem>();
			}
			int score = GameContoller.instance.GameScore;
			StartCoroutine(GameOverRoutine(score, 0.3f));
			
		}
		else {
			
			if (GameContoller.instance.state != GameState.Lost){
				if (previousHit != null){
					if (other.transform == previousHit.area && KnifeMover.instance.GetCurrentLoop() == previousHit.loop) return;
				}
				RaycastHit hit;	
				if (Physics.Raycast(new Ray(bladePosition.position, Vector3.down), out hit)){
					pool.instObject(new Vector3(hit.point.x, 2.5f, hit.point.z), 
					transform.parent.rotation);
				}
				GameContoller.instance.IncreaseScore();
				previousHit = new HitInfo{
					area = other.transform,
					loop = KnifeMover.instance.GetCurrentLoop()
				};
			}
		}
	}

	void Update()
	{
		if (p_instance){
			if (!p_instance.IsAlive()){
				Destroy(p_instance.gameObject);
			}
		}

	}

}
