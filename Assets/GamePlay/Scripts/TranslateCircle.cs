using UnityEngine;
using System.Collections;

[System.Serializable]
public class TranslateCircle {

	public float r,x,z;
	public Vector3 center;
	public float maxTime;
	public float time;

	public float lastTime = 0f;

	// Constactor
	public TranslateCircle (float r, float x, float z) {
		this.r = r;
		this.x = x;
		this.z = z;

		lastTime = 0f;
	}

	public Vector3 nextPosition (Vector3 pos) {
		return nextPosition (pos, 1f);
	}

	public Vector3 nextPosition (Vector3 pos, float speed) {
		time += Time.deltaTime / 20 * speed;
		time = time > maxTime ? time - maxTime : time;
		pos.x = r * Mathf.Cos (time) + x;
		pos.z = r * Mathf.Sin (time) + z;
		return pos;
	}

	public Quaternion focusCenter (Transform transform) {
		Quaternion _rotation = Quaternion.LookRotation (center - transform.position);
		_rotation.x = transform.rotation.x;
		_rotation.z = transform.rotation.z;
		return _rotation;
	}

	public GameObject newPillar (GameObject pillar, float grap) {
		if (lastTime == 0f) {
			lastTime = time;
		} else if (lastTime > maxTime) {
			lastTime -= maxTime;
		}
		lastTime += grap;
		Vector3 pos = pillar.transform.position;
		pos.x = r * Mathf.Cos (lastTime) + x;
		pos.z = r * Mathf.Sin (lastTime) + z;
		Quaternion rotation = Quaternion.LookRotation (center - pos);
		rotation.x = pillar.transform.rotation.x;
		rotation.z = pillar.transform.rotation.z;
		return (GameObject) GameObject.Instantiate (pillar, pos, rotation);
	}
}
