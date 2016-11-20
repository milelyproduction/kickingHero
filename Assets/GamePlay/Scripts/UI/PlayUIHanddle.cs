using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayUIHanddle : BaseUIHanddle {

	[SerializeField]private GameObject txtJump;
	[SerializeField]private GameObject gatePower;
	private Slider sliderGatePower;

	protected override void onStart () {
		sliderGatePower = gatePower.GetComponent<Slider> ();
	}

	public void setValueGatePower (float value) {
		sliderGatePower.value = value;
	}

	public void onJump () {
		if (hero.stage == HeroStage.jump) {
			hero.setStage (HeroStage.kick);
			gatePower.SetActive (false);
		} else {
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
