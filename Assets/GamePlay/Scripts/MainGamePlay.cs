using UnityEngine;
using System.Collections;

public class MainGamePlay : MonoBehaviour {

	[SerializeField]private GameObject uiStart;
	[SerializeField]private GameObject uiPlay;
	[SerializeField]private GameObject hero;
	private HeroController heroController;

	// Use this for initialization
	void Start () {
		heroController = hero.GetComponent<HeroController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
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
