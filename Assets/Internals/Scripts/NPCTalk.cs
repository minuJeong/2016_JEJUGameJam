using UnityEngine;
using System.Collections;
using DG.Tweening;

public class NPCTalk : MonoBehaviour
{

	// Use this for initialization
	void OnEnable ()
	{
		StartCoroutine ("WaitHide");

		transform.localScale = Vector3.zero;

		transform.DOScale (Vector3.one, 0.2F);
	}

	IEnumerator WaitHide ()
	{
		yield return new WaitForSeconds (1.0F);

		transform.DOScale (Vector3.zero, 0.2F);

		yield return new WaitForSeconds (0.5F);

		gameObject.SetActive (false);
	}
}

