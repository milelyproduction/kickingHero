using UnityEngine;
using System.Collections;

public class TranslateCircle : MonoBehaviour {

	public float r,x,z;
	public Transform center;
	public float maxTime;

	public float time;
	private Vector3 pos;

	// Use this for initialization
	void Start () {
		pos = gameObject.transform.position;
	}

	// Update is called once per frame
	void Update () {
		time += Time.deltaTime / 5;
		time = time > maxTime ? time - maxTime : time;
		pos.x = r * Mathf.Cos (time) + x;
		pos.z = r * Mathf.Sin (time) + z;
		transform.position = pos;
		// Look at center
		Quaternion rotation = Quaternion.LookRotation (center.position - transform.position);
		transform.rotation = rotation;
	}
}
