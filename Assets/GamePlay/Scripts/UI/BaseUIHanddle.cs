using UnityEngine;
using System.Collections;

public abstract class BaseUIHanddle : MonoBehaviour {

	protected GameObject uiStart;
	protected GameObject uiPlay;
	protected HeroController hero;

	// Use this for initialization
	void Start () {
		MainGamePlay main = GetComponent<MainGamePlay> ();
		uiStart = main.getUIStart ();
		uiPlay = main.getUIPlay ();
		hero = main.getHeroController ();

		onStart ();
	}

	protected virtual void onStart () {}
}
