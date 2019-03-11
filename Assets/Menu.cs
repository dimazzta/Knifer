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
	void Start () {
		m_Text.text = "Max. Score: " + PlayerPrefs.GetInt("maxScore");
		StartCoroutine(loadSceneAsync());
	}
	
	public void StartNewGame(){
		
		
	}

	IEnumerator loadSceneAsync(){
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("main");
		asyncOperation.allowSceneActivation = false;
		while(!asyncOperation.isDone){
			m_PlayButtonFill.fillAmount = asyncOperation.progress / 0.9f;
            if (asyncOperation.progress >= 0.9f)
            {
                m_PlayButtonBtn.interactable = true;
				m_PlayButtonBtn.onClick.AddListener(() => {
					asyncOperation.allowSceneActivation = true;
				});
			}
			yield return null;
        }
            
	}
}

