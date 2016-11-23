using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayUIHanddle : BaseUIHanddle {

	[SerializeField]private GameObject txtJump;
	[SerializeField]private Text txtScore;
	[SerializeField]private GameObject gatePower;
	private Slider sliderGatePower;

	protected override void onStart () {
		sliderGatePower = gatePower.GetComponent<Slider> ();
	}

	public void setScore (int score) {
		txtScore.text = score < 10 ? "0" + score : score.ToString ();
	}

	public void setValueGatePower (float value) {
		sliderGatePower.value = value;
	}

	public void setActiveGatePower (bool isActive) {
		gatePower.SetActive (isActive);
	}

	public void onJump () {
		if (hero.stage == HeroStage.kick) {
			return;
		} else if (hero.stage == HeroStage.jump) {
			if (sliderGatePower.value < 0.5f) {
				hero.setStage (HeroStage.run);
			} else {
				hero.setStage (HeroStage.kick);
			}
			gatePower.SetActive (false);
		} else if (hero.isJumpAgain) {
			txtJump.SetActive (false);
			sliderGatePower.value = 0f;
			gatePower.SetActive (true);
			hero.setStage (HeroStage.jump);
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
