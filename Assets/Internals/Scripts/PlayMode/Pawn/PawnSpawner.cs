using UnityEngine;
using System.Collections;

public class PawnSpawner : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		StartCoroutine ("Spawn");
	}

	IEnumerator Spawn ()
	{
		while (true)
		{
			yield return new WaitForSeconds (1.5F);

			CharacterGenerator.Instance.Generate (SavedLoader.Instance.PickModel ());
		}
	}
}
