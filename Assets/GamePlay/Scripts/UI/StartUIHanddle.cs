using UnityEngine;
using System.Collections;

public class StartUIHanddle : BaseUIHanddle {

	public void onPlay () {
		uiStart.SetActive (false);
		uiPlay.SetActive (true);
		hero.setStage (HeroStage.run);
	}

	public void onSetting () {

	}

	public void onHelp () {

	}

	public void onGift () {

	}
}
