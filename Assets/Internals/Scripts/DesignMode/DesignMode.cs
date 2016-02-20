using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DesignMode : MonoBehaviour
{
	public PreviewPixelControl m_PixelControl;

	public GameObject Design;

	public GameObject Profile;

	public InputField NameInput;

	public Text NamePreview;

	public ToastMessage ToastMessage;



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

	public void OnSave ()
	{
		// new data
		SaveModel data = new SaveModel ();
		data.SaveName = SaveName.Instance.SetName;

		// Head
		data.HeadData = new HeadData ();
		data.HeadData.TextureData = PreviewPixelControl.PartToControlMap [ChrPart.Head].GetAsTexture ().EncodeToPNG ();
		data.HeadData.JointDef = JointDisplay.Instance.HeadJointDef;

		// Body
		data.BodyData = new BodyData ();
		data.BodyData.TextureData = PreviewPixelControl.PartToControlMap [ChrPart.Body].GetAsTexture ().EncodeToPNG ();
		data.BodyData.JointDef = JointDisplay.Instance.BodyJointDef;

		// RLeg
		data.RLegData = new RLegData ();
		data.RLegData.TextureData = PreviewPixelControl.PartToControlMap [ChrPart.RLeg].GetAsTexture ().EncodeToPNG ();
		data.RLegData.JointDef = JointDisplay.Instance.RLegJointDef;

		// LLeg
		data.LLegData = new LLegData ();
		data.LLegData.TextureData = PreviewPixelControl.PartToControlMap [ChrPart.LLeg].GetAsTexture ().EncodeToPNG ();
		data.LLegData.JointDef = JointDisplay.Instance.LLegJointDef;

		// Save
		SaveCharData.SaveData (data);

		ToastMessage.ShowMessage ("Data Saved!");
	}

	public void GoDesign ()
	{
		if (!StatPoint.Instance.IsStatSafe)
		{
			Debug.Log ("Should not use more stats than available.");

			return;
		}

		if (NameInput.text == "")
		{
			Debug.Log ("Should input a name");

			return;
		}
		else
		{
			SaveName.Instance.SetName = NameInput.text;
		}


		Profile.SetActive (false);
		Design.SetActive (true);

		NamePreview.text = SaveName.Instance.SetName;
	}
}
