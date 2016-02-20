﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DesignMode : MonoBehaviour
{
	public PreviewPixelControl m_PixelControl;

	// Flow
	public GameObject Design;

	public GameObject Profile;

	// Use this for initialization
	void Start ()
	{
		// Confirm setup
		Debug.Assert (new List<UnityEngine.Object> {
			m_PixelControl
		}.TrueForAll (obj => obj != null), "DesignMode not setup");

		Profile.SetActive (true);
		Design.SetActive (false);
	}

	public void OnBack ()
	{
		SceneManager.LoadScene ("FrontEnd", LoadSceneMode.Single);	
	}

	public void GoDesign ()
	{
		Profile.SetActive (false);
		Design.SetActive (true);
	}
}
