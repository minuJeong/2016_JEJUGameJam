using UnityEditor;
using UnityEditor.SceneManagement;

public class PlayFromFrontEnd : Editor
{
	[MenuItem ("Tools/Play From FrontEnd %e")]
	public static void Run ()
	{
		EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
		EditorSceneManager.OpenScene ("Assets/Internals/Scenes/FrontEnd.unity", OpenSceneMode.Single);
		EditorApplication.isPlaying = true;
	}
}