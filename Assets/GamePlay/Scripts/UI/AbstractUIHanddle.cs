using UnityEngine;
using System.Collections;

public abstract class AbstractUIHanddle : AbstractGamePlay {

	private AudioSource audioSource;

	private void Start () {
		audioSource = gameObject.GetComponent<AudioSource> ();
	}

	protected GameObject getUIStartObject () {
		return getGamePlay ().getInstance ().uiStart;
	}

	protected GameObject getUIPlayObject () {
		return getGamePlay ().getInstance ().uiPlay;
	}

	protected GameObject getUIEndObject () {
		return getGamePlay ().getInstance ().uiEnd;
	}

	protected GameObject getUISettingObject () {
		return getGamePlay ().getInstance ().uiSetting;
	}

	protected HeroController getHeroController () {
		return getGamePlay ().getInstance ().getHeroController ();
	}

	protected void playClip (AudioClip clip) {
		audioSource.clip = clip;
		audioSource.Play ();
	}
}
