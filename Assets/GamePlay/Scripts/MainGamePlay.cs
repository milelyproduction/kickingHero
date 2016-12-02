using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainGamePlay : MonoBehaviour {

	// Inputs from Inspector
	[SerializeField]private List<AbstractGamePlay> scripts;
	[SerializeField]private InstanceGameObject instance;

	// Instances variable
	private Vector3 posCamera, posCameraZoom;
	private float pillarLastX, pillarGap;
	private int score;

	public InstanceGameObject getInstance () {
		return instance;
	}

	// Use this for initialization
	private void Start () {
		// Initialize AbstractGamePlay
		foreach (AbstractGamePlay script in scripts) {
			script.setGamePlay (this);
			instance.setScript (script);
		}
		// Set camera position
		posCamera = instance.hero.transform.position - instance.mainCamera.position;
		posCameraZoom = posCamera + new Vector3 (4f, -1f, -4f);
		// Initialize variables
		init ();
	}

	private void init () {
		pillarLastX = 10;
		pillarGap = 8;
		score = 0;
	}
	
	// Update is called once per frame
	private void Update () {
		instance.getHeroController ().update ();
		cameraTranform ();
	}

	private void cameraTranform () {
		if (instance.getHeroController ().getStage () == HeroStage.jump) {
			instance.mainCamera.position = Vector3.Slerp (instance.mainCamera.position, instance.hero.transform.position - posCameraZoom, 0.05f);
		} else {
			Vector3 pos = instance.hero.transform.position - posCamera;
			pos.y = pos.y < 1f ? 1f : pos.y;
			instance.mainCamera.position = Vector3.Slerp (instance.mainCamera.position, pos, 0.1f);
		}
	}

	public void addScore () {
		// Add score
		instance.getUIPlayHanddle ().setScore (++score);
		// Remove pillar's out of screen
		GameObject pillar = instance.pillars [0];
		instance.pillars.Remove (pillar);
		GameObject.Destroy (pillar);
		// Add new pillar
		pillarLastX += pillarGap;
		instance.pillars.Add (GameObject.Instantiate (instance.prefabPillars [Random.Range (0, instance.prefabPillars.Count - 1)]));
		Vector3 posPillar = instance.pillars [instance.pillars.Count - 1].transform.position;
		posPillar.x = pillarLastX;
		instance.pillars [instance.pillars.Count - 1].transform.position = posPillar;
		// Add new pillar target kick for hero
		instance.getHeroController ().addPillar (instance.pillars [instance.pillars.Count - 1]);
	}

	public void setGatePower (float value) {
		instance.getUIPlayHanddle ().setValueGatePower (instance.getHeroController ().gatePower);
	}

	public void didJump () {
		instance.getUIPlayHanddle ().setActiveGatePower (false);
	}

	public void endGame () {
		instance.uiPlay.SetActive (false);
		instance.uiEnd.SetActive (true);
		instance.getUIEndHanddle ().setScore (score);
	}
}

[System.Serializable]
public class InstanceGameObject {
	public GameObject uiStart;
	public GameObject uiPlay;
	public GameObject uiEnd;
	public GameObject uiSetting;
	public GameObject hero;
	public Transform mainCamera;
	public List<GameObject> pillars;
	public List<GameObject> prefabPillars;
	private UIPlayHanddle mUIPlayHanddle;
	private UIEndHanddle mUIEndHanddle;
	private HeroController mHeroController;

	public UIPlayHanddle getUIPlayHanddle () {
		return mUIPlayHanddle;
	}
	public void setUIPlayHanddle (UIPlayHanddle mUIPlayHanddle) {
		this.mUIPlayHanddle = mUIPlayHanddle;
	}
	public UIEndHanddle getUIEndHanddle () {
		return mUIEndHanddle;
	}
	public void setUIEndHanddle (UIEndHanddle mUIEndHanddle) {
		this.mUIEndHanddle = mUIEndHanddle;
	}
	public HeroController getHeroController () {
		return mHeroController;
	}
	public void setHeroController (HeroController mHeroController) {
		this.mHeroController = mHeroController;
	}

	public void setScript (AbstractGamePlay script) {
		switch (script.GetType ().ToString ()) {
		case "UIPlayHanddle":
			setUIPlayHanddle (script as UIPlayHanddle);
			break;
		case "UIEndHanddle":
			setUIEndHanddle (script as UIEndHanddle);
			break;
		case "HeroController":
			setHeroController (script as HeroController);
			break;
		}
	}
}