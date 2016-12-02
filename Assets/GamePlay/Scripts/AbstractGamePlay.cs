using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class AbstractGamePlay : MonoBehaviour {

	private MainGamePlay gamePlay;

	public MainGamePlay getGamePlay () {
		return gamePlay;
	}

	public void setGamePlay (MainGamePlay gamePlay) {
		this.gamePlay = gamePlay;
	}
}
