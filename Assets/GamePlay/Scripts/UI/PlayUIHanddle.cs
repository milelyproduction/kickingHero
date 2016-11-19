using UnityEngine;
using System.Collections;

public class PlayUIHanddle : BaseUIHanddle {

	[SerializeField]private GameObject txtJump;

	public void onJump () {
		txtJump.SetActive (false);
		hero.setStage (HeroStage.jump);
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
