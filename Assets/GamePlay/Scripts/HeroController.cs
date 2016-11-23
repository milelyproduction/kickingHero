using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroController : MonoBehaviour {

	public bool isJumpAgain;
	[SerializeField]private GameObject hero;
	[SerializeField]private Animator anim;
	[SerializeField]private float runSpeed;
	[SerializeField]private HeroAnim heroAnim;
	[SerializeField]private MainGamePlay main;
	public HeroStage stage { get; private set; }
	private Vector3 dirRun, dirJump;
	private float time;
	public float gatePower { get; private set; }
	[SerializeField]private List<Transform> targetKicks;
	private Rigidbody rigid;

	public void setStage(HeroStage stage) {
		onChangeStage (this.stage, stage);
		this.stage = stage;
	}

	public void addTarget (Transform transform) {
		targetKicks.Add (transform);
	}

	public void end () {
		rigid.useGravity = false;
		rigid.velocity = Vector3.zero;
		setStage (HeroStage.stop);
		hero.SetActive (false);
	}

	// Use this for initialization
	private void Start () {
		stage = HeroStage.start;
		runSpeed = runSpeed <= 0f ? 1f : runSpeed;
		dirRun = new Vector3 (runSpeed / 100f, 0f, 0f);
		dirJump = dirRun + Vector3.up * 0.08f;
		rigid = GetComponent<Rigidbody> ();
		isJumpAgain = true;
	}
	
	// Update is called once per frame
	private void Update () {
		if (stage == HeroStage.start) {
			onStart ();
		} else if (stage == HeroStage.run) {
			onRun ();
		} else if (stage == HeroStage.jump) {
			onJump ();
		} else if (stage == HeroStage.kick) {
			onKick ();
		} else {
			onStop ();
		}

		if (hero.transform.position.y < -1f) {
			main.endGame ();
		}
	}

	private void jumpAgain () {
		isJumpAgain = true;
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.transform.tag == "Target") {
			Invoke ("jumpAgain", 0.1f);
			if (stage == HeroStage.jump || stage == HeroStage.kick) {
				setStage (HeroStage.run);
				if (collision.transform == targetKicks [0]) {
					nextTarget ();
				}
			}
		}
	}

	private void nextTarget () {
		targetKicks.Remove (targetKicks [0]);
		main.addScore ();
	}

	private void onChangeStage (HeroStage oldStage, HeroStage newStage) {
		if (oldStage == newStage) {
			return;
		} else if (newStage == HeroStage.jump && !isWillJump ()) {
			return;
		} else if (oldStage == HeroStage.jump) {
			didJump ();
		}
		// Change Anim
//		anim.SetBool (heroAnim.getAnim (oldStage), false);
		anim.SetBool (heroAnim.getAnim (newStage), true);
		time = 0f;
	}

	private void onStart () {

	}

	private void onRun () {
		hero.transform.Translate (dirRun);
	}

	private bool isWillJump () {
		if (rigid.velocity.y < -0.5f) {
			setStage (HeroStage.run);
			return false;
		}
		gatePower = 0f;
		rigid.useGravity = false;
		anim.speed = 0.5f;
		isJumpAgain = false;
		return true;
	}

	private void onJump () {
		hero.transform.Translate (dirJump);
		time += Time.deltaTime;
		gatePower = time * 1f / 1.15f;
		main.setGatePower (gatePower);
		if (gatePower > 1f) {
			setStage (HeroStage.run);
		}
	}

	private void didJump () {
		gatePower = 0f;
		anim.speed = 1f;
		rigid.useGravity = true;
		main.didJump ();
	}

	private void onKick () {
		hero.transform.position = Vector3.Lerp (hero.transform.position, targetKicks[0].position, 0.25f);
	}

	private void onStop () {

	}


}

public enum HeroStage {
	start = 0,
	run = 1,
	jump = 2,
	kick = 3,
	stop = 4
}

[System.Serializable]
public class HeroAnim {
	public string waveHand = "WaveHand";
	public string idle = "Idle";
	public string run = "Run";
	public string jump = "Jump";
	public string dash = "Dash";
	public string relax = "Relax";

	public string getAnim (HeroStage stage) {
		switch (stage) {
		case HeroStage.start:
			return idle;
		case HeroStage.run:
			return run;
		case HeroStage.jump:
			return jump;
		case HeroStage.kick:
			return dash;
		case HeroStage.stop:
			return relax;
		default:
			return idle;
		}
	}
}