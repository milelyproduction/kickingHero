using UnityEngine;
using System.Collections;

public class UIStartHanddle : AbstractUIHanddle {

	public void onPlay () {
		getHeroController ().setStage (HeroStage.run);
		getUIStartObject ().SetActive (false);
		getUIPlayObject ().SetActive (true);
	}
}
