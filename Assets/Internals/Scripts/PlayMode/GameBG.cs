using UnityEngine;
using System.Collections;

public class GameBG : MonoBehaviour
{
	public Sprite[] BGCandidates;

	// Use this for initialization
	void Start ()
	{
		int index = Random.Range (0, BGCandidates.Length);

		GetComponent<SpriteRenderer> ().sprite = BGCandidates [index];
	}
}
