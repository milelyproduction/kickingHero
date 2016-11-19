using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {

	[SerializeField]private GameObject hero;
	[SerializeField]private Animator anim;
	[SerializeField]private float runSpeed;
	[SerializeField]private HeroAnim heroAnim;
	private HeroStage stage;
	private Vector3 dirRun, dirJump, dirKick;
	private float time;
	[SerializeField]private Transform targetKick;
	private Rigidbody rigid;

	public void setStage(HeroStage stage) {
		onChangeStage (this.stage, stage);
		this.stage = stage;
	}

	// Use this for initialization
	private void Start () {
		stage = HeroStage.start;
		runSpeed = runSpeed <= 0f ? 1f : runSpeed;
		dirRun = new Vector3 (runSpeed / 100f, 0f, 0f);
		dirJump = dirRun + Vector3.up * 0.05f;
		dirKick = new Vector3 ((runSpeed / 100f) * 5f, -0.2f, 0f);
		rigid = GetComponent<Rigidbody> ();
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
	}

	private void OnCollisionEnter(Collision collision) {
		rigid.useGravity = true;
		if (stage == HeroStage.jump) {
			setStage (HeroStage.run);
		}
	}

	private void onChangeStage (HeroStage oldStage, HeroStage newStage) {
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

	private void onJump () {
		rigid.useGravity = false;
		if (time < 2.3f) {
			anim.speed = 0.2f;
			hero.transform.Translate (dirJump);
			time += Time.deltaTime;
		} else {
			anim.speed = 1f;
			hero.transform.position = Vector3.Lerp (hero.transform.position, targetKick.position, 0.25f);
		}
	}

	private void onKick () {

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