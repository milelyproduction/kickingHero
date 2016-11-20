using UnityEngine;
using System.Collections;

public class PillarManager : MonoBehaviour {

	[SerializeField]private Transform target;

	public Transform getTarget () {
		return target;
	}
}
