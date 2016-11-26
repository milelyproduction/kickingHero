using UnityEngine;
using System.Collections;

public class UIStartHanddle : AbstractUIHanddle {

	public void onPlay () {
		getHeroController ().setStage (HeroStage.run);
		getUIStartObject ().SetActive (false);
		getUIPlayObject ().SetActive (true);
	}

	public void onSetting () {

	}

	public void onHelp () {

	}

	public void onGift () {

	}
}
