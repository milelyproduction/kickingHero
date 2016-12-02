using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPlayHanddle : AbstractUIHanddle {

	[SerializeField]private GameObject textJump;
	[SerializeField]private Text textScore;
	[SerializeField]private GameObject powerBar;
	private Slider sliderPowerBar;

	// Use this for initialization 
	private void Start () {
		sliderPowerBar = powerBar.GetComponent<Slider> ();
	}

	public void setScore (int score) {
		textScore.text = score < 10 ? "0" + score : score.ToString ();
	}

	public void setValueGatePower (float value) {
		sliderPowerBar.value = value;
	}

	public void setActiveGatePower (bool isActive) {
		powerBar.SetActive (isActive);
	}

	public void onJump () {
		switch (getHeroController ().getStage ()) {
		case HeroStage.jump:
			if (sliderPowerBar.value < 0.5f) {
				getHeroController ().setStage (HeroStage.run);
			} else {
				getHeroController ().setStage (HeroStage.kick);
			}
			powerBar.SetActive (false);
			break;
		case HeroStage.kick:
			break;
		default:
			if (getHeroController ().isCanJump ()) {
				textJump.SetActive (false);
				sliderPowerBar.value = 0f;
				powerBar.SetActive (true);
				getHeroController ().setStage (HeroStage.jump);
			}
			break;
		}
	}

	public void onAttact () {

	}

	public void onPuase () {

	}

	public void onResume () {

	}

	public void onSetting () {

	}
}