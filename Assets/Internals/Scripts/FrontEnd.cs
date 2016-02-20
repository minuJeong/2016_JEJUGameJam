using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class FrontEnd : MonoBehaviour
{
	public void GoMode (string SceneName)
	{
		SceneManager.LoadScene (SceneName, LoadSceneMode.Single);
	}
}
