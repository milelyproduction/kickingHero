using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIStartHanddle : AbstractUIHanddle {

	[SerializeField]private GameObject btnStart;
	[SerializeField]private Text txtCountDown; 
	private int countDown = 3;

	public void onPlay () {
		btnStart.SetActive (false);
		txtCountDown.enabled = true;
		Invoke ("callbackCountDown", 1f);
	}

	private void callbackCountDown () {
		if (--countDown == -1) {
			txtCountDown.enabled = false;
			getUIStartObject ().SetActive (false);
			getUIPlayObject ().SetActive (true);
			return;
		} else if (countDown > 0) {
			txtCountDown.text = countDown.ToString ();
			Invoke ("callbackCountDown", 1f);
			if (countDown == 1) {
				getGamePlay ().onStart ();
				getHeroController ().setStage (HeroStage.run);
			}
		} else {
			txtCountDown.text = "Go!";
			Invoke ("callbackCountDown", 1f);
		}
	}
}
