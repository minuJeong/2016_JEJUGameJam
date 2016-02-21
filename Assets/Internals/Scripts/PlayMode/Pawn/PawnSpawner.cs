using UnityEngine;
using System.Collections;

public class PawnSpawner : MonoBehaviour
{
	public static PawnSpawner Instance;

	public AnimationClip Idle;

	public AnimationClip Walk;

	public AnimationClip Attack;

	public GameObject LightSabrePrefab;

	void Awake ()
	{
		Instance = this;
	}

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
