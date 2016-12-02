using UnityEngine;
using System.Collections;

public abstract class AbstractUIHanddle : AbstractGamePlay {

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
}
