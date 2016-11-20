using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainGamePlay : MonoBehaviour {

	[SerializeField]private GameObject uiStart;
	[SerializeField]private GameObject uiPlay;
	[SerializeField]private GameObject hero;
	[SerializeField]private Transform camera;
	[SerializeField]private List<GameObject> pillars;
	[SerializeField]private List<GameObject> prefabPillars;
	private PlayUIHanddle playUIHanddle;
	private HeroController heroController;
	private Vector3 posCamera, posCameraZoom;
	private int score;
	private float lastPillarX;

	// Use this for initialization
	void Awake () {
		playUIHanddle = GetComponent<PlayUIHanddle> ();
		heroController = hero.GetComponent<HeroController> ();
		posCamera = hero.transform.position - camera.position;
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
			camera.position = Vector3.Slerp (camera.position, hero.transform.position - posCameraZoom, 0.05f);
		} else {
			camera.position = Vector3.Slerp (camera.position, hero.transform.position - posCamera, 0.5f);
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
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
