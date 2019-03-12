using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>

	public Text m_Text;
	public Image m_PlayButtonFill;
	public Button m_PlayButtonBtn;
	public static Menu instance;
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
	}
	void Start () {
		UpdateMaxScore();
		ShowMenu();
	}
	public void UpdateMaxScore(){
		m_Text.text = "Max. Score: " + PlayerPrefs.GetInt("maxScore");
	}
	public void ShowMenu(){
		UpdateMaxScore();
		AmbientSoundController.instance.TurnOff();
		gameObject.SetActive(true);
		Time.timeScale = 0;
		PrepareScene();

	}
	public void PrepareScene(){
		KnifeMover.instance.SetupMovement();
		GameContoller.instance.ClearScene();
		GameContoller.instance.ResetScore();
		GameContoller.instance.state = GameState.InMenu;
		TriggerHandler.instance.ResetPreviousHit();
	}
	public void StartNewGame(){
		AmbientSoundController.instance.TurnOn();
		GameContoller.instance.state = GameState.Playing;
		gameObject.SetActive(false);
		KnifeMover.instance.StartMovement();
		Time.timeScale = 1;
	}
}

