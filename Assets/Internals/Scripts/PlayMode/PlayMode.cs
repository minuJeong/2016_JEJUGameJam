using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMode : MonoBehaviour
{

	public void OnBack ()
	{
		SceneManager.LoadScene ("FrontEnd", LoadSceneMode.Single);	
	}
}
