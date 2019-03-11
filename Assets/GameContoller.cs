using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameContoller : MonoBehaviour {
	public void GameOver(int score = 0){
		int maxScore = PlayerPrefs.GetInt("maxScore");
		if (score > maxScore) 
			PlayerPrefs.SetInt("maxScore", score);
		PlayerPrefs.SetInt("lastScore", score);
		SceneManager.LoadScene("menu");
	}
}
