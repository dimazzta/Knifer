using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState {Lost, InMenu, Playing};
public class GameContoller : MonoBehaviour {
	public static GameContoller instance;
	public GameState state { get; set; }
	private int gameScore;
	public int GameScore {get
		{
			return gameScore;
		} 
		set
		{
			gameScore = value;
		}
	}

	public Text scoreText;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		if (instance == null){
			instance = this;
		}
		else if (instance != this){
			Destroy(gameObject);
		}
		gameScore = 0;
	}
	public void GameOver(int score = 0){
		int maxScore = PlayerPrefs.GetInt("maxScore");
		if (score > maxScore) 
			PlayerPrefs.SetInt("maxScore", score);
		PlayerPrefs.SetInt("lastScore", score);
		Menu.instance.ShowMenu();
	}


	public int IncreaseScore(){
		++gameScore;
		UpdateScoreText();
		return gameScore;
	}

	public void ResetScore(){
		gameScore = 0;
		UpdateScoreText();
	}
	private void UpdateScoreText(){
		scoreText.text = gameScore.ToString();
	}
	public void ClearScene(){
		GameObject[] slivers = GameObject.FindGameObjectsWithTag("Sliver");
		GameObject[] bloods = GameObject.FindGameObjectsWithTag("Blood");
		foreach (var sliver in slivers){
			Destroy(sliver);
		}
		foreach (var blood in bloods){
			Destroy(blood);
		}
	}

}
