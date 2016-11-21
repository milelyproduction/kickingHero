using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadLevelCsharp : MonoBehaviour 
{

	public string levelName;

	public void LoadLevelNow (string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}
}
