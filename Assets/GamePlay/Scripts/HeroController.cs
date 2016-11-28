using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroController : AbstractGamePlay {

	// Inputs inspector
	[SerializeField]private GameObject heroObject;
	[SerializeField]private Animator animator;
	[SerializeField]private HeroAnim heroAnim;
	[SerializeField]private float runSpeed;
	[SerializeField]private List<Transform> targetKicks;

	// Instances variable set on start
	private Vector3 dirRun, dirJump;
	private Rigidbody rigid;

	// variables
	private HeroStage stage;
	private float time;
	private bool isJumpAgain;

	public float gatePower { get; private set; }

	// Use this for initialization
	private void Start () {
		stage = HeroStage.start;
		runSpeed = runSpeed <= 0f ? 1f : runSpeed;
		dirRun = new Vector3 (runSpeed / 100f, 0f, 0f);
		dirJump = dirRun + Vector3.up * 0.08f;
		rigid = heroObject.GetComponent<Rigidbody> ();
		isJumpAgain = true;
	}

	public bool isCanJump () {
		return isJumpAgain && rigid.velocity.y > -0.5f;
	}

	public void addPillar (GameObject pillar) {
		targetKicks.Add (pillar.GetComponent<PillarManager> ().getTarget ());
	}

	public HeroStage getStage () {
		return stage;
	}

	public void setStage(HeroStage stage) {
		onChangeStage (this.stage, stage);
		this.stage = stage;
	}

	public void end () {
		rigid.useGravity = false;
		rigid.velocity = Vector3.up;
		setStage (HeroStage.stop);
		heroObject.SetActive (false);
	}

	// Update is called once per frame
	public void update () {
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

		if (heroObject.transform.position.y < -1f && heroObject.activeSelf) {
			end ();
			getGamePlay ().endGame ();
		}
	}

	private void onChangeStage (HeroStage oldStage, HeroStage newStage) {
		if (oldStage == newStage || (newStage == HeroStage.jump && !isWillJump ())) {
			return;
		} else if (oldStage == HeroStage.jump) {
			didJump ();
		}
		// Change Anim
		animator.SetBool (heroAnim.getAnim (newStage), true);
		time = 0f;
	}
		
	private void OnCollisionEnter (Collision collision) {
		enterPillar (collision);
		enterEnemy (collision);
	}

	private void enterPillar (Collision collision) {
		if (collision.transform.tag == "Target" || collision.transform.tag == "Pillar") {
			Invoke ("jumpAgain", 0.1f);
			if (stage == HeroStage.jump || stage == HeroStage.kick) {
				setStage (HeroStage.run);
			}
			if (collision.transform == targetKicks [0]) {
				nextTarget ();
			}
		}
	}

	private void enterEnemy (Collision collision) {
		if (collision.transform.tag == "Enemy") {
			if (stage == HeroStage.kick) {
				Destroy (collision.gameObject);
			}
		}
	}

	private void nextTarget () {
		targetKicks.Remove (targetKicks [0]);
		getGamePlay ().addScore ();
	}

	private void jumpAgain () {
		isJumpAgain = true;
	}

	private void onStart () {

	}

	private void onRun () {
		heroObject.transform.Translate (dirRun);
	}

	private bool isWillJump () {
		if (!isCanJump ()) {
			setStage (HeroStage.run);
			return false;
		}
		gatePower = 0f;
		rigid.useGravity = false;
		animator.speed = 0.5f;
		isJumpAgain = false;
		return true;
	}

	private void onJump () {
		heroObject.transform.Translate (dirJump);
		time += Time.deltaTime;
		gatePower = time * 1f / 1.15f;
		getGamePlay ().setGatePower (gatePower);
		if (gatePower > 1f) {
			setStage (HeroStage.run);
		}
	}

	private void didJump () {
		gatePower = 0f;
		animator.speed = 1f;
		rigid.useGravity = true;
		getGamePlay ().didJump ();
	}

	private void onKick () {
		heroObject.transform.position = Vector3.Lerp (heroObject.transform.position, targetKicks[0].position, 0.25f);
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