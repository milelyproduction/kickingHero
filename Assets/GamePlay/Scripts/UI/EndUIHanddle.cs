using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndUIHanddle : BaseUIHanddle {

	[SerializeField]private Text txtScore;

	public void setScore (int score) {
		txtScore.text = score < 10 ? "0" + score : score.ToString ();
	}

	public void onSetting () {

	}

	public void onHome () {
		SceneManager.LoadScene (0);
	}

	public void onReplay () {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void onGift () {

	}
}