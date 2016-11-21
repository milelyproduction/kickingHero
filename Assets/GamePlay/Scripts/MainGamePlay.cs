using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainGamePlay : MonoBehaviour {

	[SerializeField]private GameObject uiStart;
	[SerializeField]private GameObject uiPlay;
	[SerializeField]private GameObject uiEnd;
	[SerializeField]private GameObject hero;
	[SerializeField]private Transform mainCamera;
	[SerializeField]private List<GameObject> pillars;
	[SerializeField]private List<GameObject> prefabPillars;
	private PlayUIHanddle playUIHanddle;
	private EndUIHanddle endUIHanddle;
	private HeroController heroController;
	private Vector3 posCamera, posCameraZoom;
	private int score;
	private float lastPillarX;

	// Use this for initialization
	void Awake () {
		playUIHanddle = GetComponent<PlayUIHanddle> ();
		endUIHanddle = GetComponent<EndUIHanddle> ();
		heroController = hero.GetComponent<HeroController> ();
		posCamera = hero.transform.position - mainCamera.position;
		posCameraZoom = posCamera;
		posCameraZoom.x += 4;
		posCameraZoom.y -= 1;
		posCameraZoom.z -= 4;
		score = 0;
		lastPillarX = 6;
	}
	
	// Update is called once per frame
	void Update () {
		if (heroController.stage == HeroStage.jump) {
			mainCamera.position = Vector3.Slerp (mainCamera.position, hero.transform.position - posCameraZoom, 0.05f);
		} else {
			Vector3 pos = hero.transform.position - posCamera;
			pos.y = pos.y < 1f ? 1f : pos.y;
			mainCamera.position = Vector3.Slerp (mainCamera.position, pos, 0.1f);
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

	public void addScore () {
		playUIHanddle.setScore (++score);

		lastPillarX += 6;
		GameObject pillar = pillars [0];
		pillars.Remove (pillar);
		GameObject.Destroy (pillar);
		pillars.Add (GameObject.Instantiate (prefabPillars [Random.Range (0, prefabPillars.Count - 1)]));
		Vector3 posPillar = pillars [pillars.Count - 1].transform.position;
		posPillar.x = lastPillarX;
		pillars [pillars.Count - 1].transform.position = posPillar;
		heroController.addTarget (pillars [pillars.Count - 1].GetComponent<PillarManager> ().getTarget());
	}

	public void setGatePower (float value) {
		playUIHanddle.setValueGatePower (heroController.gatePower);
	}

	public void didJump () {
		playUIHanddle.setActiveGatePower (false);
	}

	public void endGame () {
		uiPlay.SetActive (false);
		uiEnd.SetActive (true);
		endUIHanddle.setScore (score);
		heroController.end ();
	}
}
