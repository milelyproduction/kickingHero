using UnityEngine;
using System.Collections;

public class LoadLevelCsharp : MonoBehaviour 
{

	public string levelName;

	public void LoadLevelNow ( string x)
	{
		Application.LoadLevel (x);
	}
}
