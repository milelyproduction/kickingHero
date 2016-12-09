using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class UISettingHanddle : AbstractUIHanddle {

	[SerializeField]private GameObject btnPlay;
	[SerializeField]private Text txtCountDown;
	[SerializeField]private Button btnSound;
	[SerializeField]private Button btnMute;
	[SerializeField]private List<AudioSource> audios;
	[SerializeField]private List<Image> volumes;
	private int count;
	private int volume = 2;

	public void setActive (bool isActive) {
		bool isPlaying = (getHeroController ().getStage () != HeroStage.start);
		btnPlay.SetActive (isPlaying);
		getUISettingObject ().SetActive (isActive);
		if (isActive) {
			getUIStartObject ().SetActive (false);
			getUIPlayObject ().SetActive (false);
			if (isPlaying) {
				Time.timeScale = 0;
				count = 3;
			}
		}
	}

	public void playAgain () {
		getUISettingObject ().SetActive (false);
		getUIPlayObject ().SetActive (true);
//		Invoke ("countDown", 1f);
		Time.timeScale = 1;
		txtCountDown.enabled = true;
	}

	private void countDown () {
		if (--count > 0) {
			txtCountDown.text = count.ToString ();
			Invoke ("countDown", 1f);
		} else {
			txtCountDown.enabled = false;
			Time.timeScale = 1;
		}
	}

	public void back () {
		Time.timeScale = 1;
		getUISettingObject ().SetActive (false);
		if (getHeroController ().getStage () == HeroStage.start) {
			getUIStartObject ().SetActive (true);
		} else {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}

	public void setSound (bool isSound) {
		btnSound.interactable = !isSound;
		btnMute.interactable = isSound;
		foreach (AudioSource audio in audios) {
			audio.enabled = isSound;
		}
	}

	public void setVolume (bool isAdd) {
		if (isAdd && volume < volumes.Count) {
			volume++;
		} else if (!isAdd && volume > 0) {
			volume--;
		}
		for (int i = 0; i < volumes.Count; i++) {
			volumes [i].enabled = volume > i;
		}

		float v = 1f / volumes.Count * volume;
		foreach (AudioSource audio in audios) {
			audio.volume = v;
		}
	}
}
