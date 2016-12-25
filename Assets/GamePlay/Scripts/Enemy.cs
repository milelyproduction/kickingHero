using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[SerializeField]private Animator anim;

	private void OnCollisionEnter (Collision collision) {
//		Debug.Log ("Collision tag: " + collision.transform.tag);
		if (collision.transform.tag == "Attack") {
			death ();
		}
	}

	private void OnTriggerEnter (Collider collider) {
//		Debug.Log ("Collider tag: " + collider.transform.tag);
		if (collider.transform.tag == "Attack") {
			death ();
		}
	}

	public void death () {
		if (anim == null) {
			GetComponent<Animator> ().SetTrigger ("Die");
		} else {
			anim.SetTrigger ("Die");
		}
		GetComponent<Rigidbody> ().useGravity = true;
		GetComponent<Collider> ().isTrigger = true;
	}
}
