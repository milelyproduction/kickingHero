using UnityEngine;
using System.Collections;

public class MainGamePlay : MonoBehaviour {

	[SerializeField]private GameObject uiStart;
	[SerializeField]private GameObject uiPlay;
	[SerializeField]private GameObject hero;
	private PlayUIHanddle playUIHanddle;
	private HeroController heroController;

	// Use this for initialization
	void Start () {
		playUIHanddle = GetComponent<PlayUIHanddle> ();
		heroController = hero.GetComponent<HeroController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (heroController.stage == HeroStage.jump) {
			playUIHanddle.setValueGatePower (heroController.gatePower);
		} else {
			playUIHanddle.setActiveGatePower (false);
		}
	}

	public GameObject getUIStart () {
		return uiStart;
	}

	public GameObject getUIPlay () {
		return uiPlay;
	}

	public HeroController getHeroController () {
		return heroController;
	}
}
