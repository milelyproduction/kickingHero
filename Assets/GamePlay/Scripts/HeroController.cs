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
	[SerializeField]private Collider colAttack;
	public Rigidbody rigid;

	// Instances variable set on start
	private Vector3 dirRun, dirJump;

	// variables
	private HeroStage stage;
	private float time;
	private bool isJumpAgain;

	public float gatePower { get; private set; }

	// Use this for initialization
	private void Start () {
		stage = HeroStage.start;
		runSpeed = runSpeed <= 0f ? 1f : runSpeed;
		dirJump = Vector3.up * 0.12f;
//		rigid = heroObject.GetComponent<Rigidbody> ();
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
		setRunDir ();
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
			transform.position = Vector3.Lerp (transform.position, heroObject.transform.position, 1f);
		} else if (stage == HeroStage.kick) {
			onKick ();
			transform.position = Vector3.Lerp (transform.position, heroObject.transform.position, 0.2f);
		} else {
			onStop ();
		}

		heroRotate ();

		if (heroObject.transform.position.y < -1f && heroObject.activeSelf) {
			end ();
			getGamePlay ().endGame ();
		}
	}

	private void heroRotate () {
		heroObject.transform.rotation = getGamePlay ().getTranslateCircle ().focusCenter (heroObject.transform);
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
			if (stage == HeroStage.jump || stage == HeroStage.kick) {
				Invoke ("jumpAgain", 0.1f);
				setStage (HeroStage.run);
				while (true) {
					Vector3 hero = heroObject.transform.position;
					Vector3 circle = getGamePlay ().getTranslateCircle ().nextPosition (hero);
					float area = 1f;
					if (
						circle.x > hero.x - area && circle.x < hero.x + area &&
						circle.z > hero.z - area && circle.z < hero.z + area
					) {
						circle = getGamePlay ().getTranslateCircle ().nextPosition (hero);
						while (area > 0f) {
							area -= 0.05f;
							if ((circle.x < hero.x - area || circle.x > hero.x + area) ||
								(circle.z < hero.z - area || circle.z > hero.z + area)) {
								circle = getGamePlay ().getTranslateCircle ().nextPosition (hero);
							}
						}
						break;
					}
				}
			}
			if (targetKicks.Count > 0 && collision.transform == targetKicks [0]) {
				nextTarget ();
			}
		}
	}

	private void enterEnemy (Collision collision) {
		if (collision.transform.tag == "Enemy") {
			if (stage == HeroStage.kick) {
//				Destroy (collision.gameObject);
				collision.gameObject.SendMessage ("death");
			} else {
				death ();
			}
		}
	}

	private void nextTarget () {
		targetKicks.Remove (targetKicks [0]);
		getGamePlay ().addScore ();
	}

	public void setRunDir () {
		dirRun = targetKicks [0].position - heroObject.transform.position;
		dirRun = dirRun.normalized / 100;
	}

	private void jumpAgain () {
		isJumpAgain = true;
	}

	private void onStart () {
		heroObject.transform.position = getGamePlay ().getTranslateCircle ().nextPosition (heroObject.transform.position);
	}

	private void onRun () {
		heroObject.transform.position = getGamePlay ().getTranslateCircle ().nextPosition (heroObject.transform.position);
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
		heroObject.transform.Translate (dirJump + dirRun * runSpeed);
		heroObject.transform.position = getGamePlay ().getTranslateCircle ().nextPosition (heroObject.transform.position);
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
		heroObject.transform.position = Vector3.Lerp (heroObject.transform.position, targetKicks[0].position, 0.2f);
	}

	public void attack () {
		if (stage == HeroStage.run) {
			colAttack.enabled = true;
			setStage (HeroStage.attack);
			Invoke ("didAttack", 0.8f);
		}
	}

	private void didAttack () {
		colAttack.enabled = false;
		setStage (HeroStage.run);
	}

	private void onStop () {

	}

	private void death () {
		setStage (HeroStage.death);
		getGamePlay ().endGame ();
	}


}

public enum HeroStage {
	start = 0,
	run = 1,
	jump = 2,
	kick = 3,
	stop = 4,
	attack = 5,
	death = 6
}

[System.Serializable]
public class HeroAnim {
	public string waveHand = "WaveHand";
	public string idle = "Idle";
	public string run = "Run";
	public string jump = "Jump";
	public string dash = "Dash";
	public string relax = "Relax";
	public string attack = "Attack";
	public string death = "Die";

	public string getAnim (HeroStage stage) {
		switch (stage) {
		case HeroStage.start:
			return run;
		case HeroStage.run:
			return run;
		case HeroStage.jump:
			return jump;
		case HeroStage.kick:
			return dash;
		case HeroStage.stop:
			return relax;
		case HeroStage.attack:
			return attack;
		case HeroStage.death:
			return death;
		default:
			return idle;
		}
	}
}