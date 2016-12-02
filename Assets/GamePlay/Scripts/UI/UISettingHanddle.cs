using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UISettingHanddle : AbstractUIHanddle {

	[SerializeField]private GameObject btnPlay;

	public void setActive (bool isActive) {
		bool isPlaying = (getHeroController ().getStage () != HeroStage.start);
		btnPlay.SetActive (isPlaying);
		getUISettingObject ().SetActive (isActive);
		if (isActive) {
			getUIStartObject ().SetActive (false);
			getUIPlayObject ().SetActive (false);
		}
	}

	public void playAgain () {
		getUISettingObject ().SetActive (false);
		getUIPlayObject ().SetActive (true);
	}

	public void back () {
		getUISettingObject ().SetActive (false);
		if (getHeroController ().getStage () == HeroStage.start) {
			getUIStartObject ().SetActive (true);
		} else {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}
}
