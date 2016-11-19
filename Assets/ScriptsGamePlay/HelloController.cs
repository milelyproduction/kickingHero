using UnityEngine;
using System.Collections;

public class HelloController : MonoBehaviour {

	public GameObject hero;
	private Stage stage;

	// Use this for initialization
	void Start () {
		stage = Stage.start;
	}
	
	// Update is called once per frame
	void Update () {
		if (stage == Stage.start) {
			onStart ();
		} else if (stage == Stage.jump) {
			onJump ();
		} else if (stage == Stage.kick) {
			onKick ();
		} else {
			onStop ();
		}
	}

	private void onStart () {

	}

	private void onJump () {

	}

	private void onKick () {

	}

	private void onStop () {

	}
}

enum Stage {
	start = 0,
	jump = 1,
	kick = 2,
	stop = 3
}