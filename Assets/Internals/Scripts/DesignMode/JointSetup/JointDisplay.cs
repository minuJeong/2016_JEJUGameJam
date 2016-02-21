using UnityEngine;
using System.Collections.Generic;

public class JointDisplay : MonoBehaviour
{
	public static JointDisplay Instance;

	public GameObject KnobTemplate;

	public HeadJointDef HeadJointDef = new HeadJointDef ();
	public BodyJointDef BodyJointDef = new BodyJointDef ();
	public RLegJointDef RLegJointDef = new RLegJointDef ();
	public LLegJointDef LLegJointDef = new LLegJointDef ();

	readonly protected Dictionary<PartJointDef, List<GameObject>> DictMap = new Dictionary<PartJointDef, List<GameObject>> ();

	protected readonly Dictionary<GameObject, float[]> KnobToDataMap = new Dictionary<GameObject, float[]> ();

	protected readonly List<GameObject> Knobs = new List<GameObject> ();

	void Awake ()
	{
		Instance = this;

		HeadJointDef.Neck = new float[2];
		BodyJointDef.Neck = new float[2];
		BodyJointDef.RLegPelvis = new float[2];
		BodyJointDef.LLegPelvis = new float[2];
		RLegJointDef.Pelvis = new float[2];
		LLegJointDef.Pelvis = new float[2];
	}

	public static void ChangeJointDisplayDef (ChrPart part)
	{
		if (DesignModeSelector.Instance.DMode != DesignDetailMode.JointMode)
		{
			return;
		}

		if (part == ChrPart.Head)
		{
			Instance.Display_HeadJointDef ();
		}
		else
		if (part == ChrPart.Body)
		{
			Instance.Display_BodyJointDef ();
		}
		else
		if (part == ChrPart.RLeg)
		{
			Instance.Display_RLegJointDef ();
		}
		else
		if (part == ChrPart.LLeg)
		{
			Instance.Display_LLegJointDef ();
		}

		Instance.UpdateDataValues ();
	}

	protected void UpdateDataValues ()
	{
		if (DictMap.ContainsKey (HeadJointDef))
		{
			HeadJointDef.Neck [0] = DictMap [HeadJointDef] [0].GetComponent<JointKnob> ().xRatio;
			HeadJointDef.Neck [1] = DictMap [HeadJointDef] [0].GetComponent<JointKnob> ().yRatio;
		}

		if (DictMap.ContainsKey (BodyJointDef))
		{
			BodyJointDef.Neck [0] = DictMap [BodyJointDef] [0].GetComponent<JointKnob> ().xRatio;
			BodyJointDef.Neck [1] = DictMap [BodyJointDef] [0].GetComponent<JointKnob> ().yRatio;

			BodyJointDef.RLegPelvis [0] = DictMap [BodyJointDef] [1].GetComponent<JointKnob> ().xRatio;
			BodyJointDef.RLegPelvis [1] = DictMap [BodyJointDef] [1].GetComponent<JointKnob> ().yRatio;

			BodyJointDef.LLegPelvis [0] = DictMap [BodyJointDef] [2].GetComponent<JointKnob> ().xRatio;
			BodyJointDef.LLegPelvis [1] = DictMap [BodyJointDef] [2].GetComponent<JointKnob> ().yRatio;
		}

		if (DictMap.ContainsKey (RLegJointDef))
		{
			RLegJointDef.Pelvis [0] = DictMap [RLegJointDef] [0].GetComponent<JointKnob> ().xRatio;
			RLegJointDef.Pelvis [1] = DictMap [RLegJointDef] [0].GetComponent<JointKnob> ().yRatio;
		}

		if (DictMap.ContainsKey (LLegJointDef))
		{
			LLegJointDef.Pelvis [0] = DictMap [LLegJointDef] [0].GetComponent<JointKnob> ().xRatio;
			LLegJointDef.Pelvis [1] = DictMap [LLegJointDef] [0].GetComponent<JointKnob> ().yRatio;
		}
	}

	public void OnUpdateJointKnobPosition ()
	{
		var keyCollection = KnobToDataMap.Keys;
		var keys = new List<GameObject> ();
		foreach (var key in keyCollection)
		{
			keys.Add (key);
		}

		int len = keys.Count;
		for (int i = 0; i < len; i++)
		{
			if (keys [i] == null)
			{
				continue;
			}

			KnobToDataMap [keys [i]] = new float[] {
				keys [i].GetComponent<JointKnob> ().xRatio,
				keys [i].GetComponent<JointKnob> ().yRatio
			};
		}
	}


	void Display_HeadJointDef ()
	{
		GameObject knob_Neck;

		if (DictMap.ContainsKey (HeadJointDef))
		{
			knob_Neck = DictMap [HeadJointDef] [0];

			knob_Neck.SetActive (true);
		}
		else
		{
			knob_Neck = Instantiate (KnobTemplate);

			DictMap.Add (HeadJointDef, new List<GameObject> ());
			DictMap [HeadJointDef].Add (knob_Neck);

			knob_Neck.transform.SetParent (transform);
			knob_Neck.transform.localPosition = Vector3.zero;
			knob_Neck.name = "Head-Neck";
			knob_Neck.GetComponent<JointKnob> ().Comment.text = "Head-Neck";
		}

		knob_Neck.SetActive (true);

		// Turn off
		if (DictMap.ContainsKey (BodyJointDef))
		{
			DictMap [BodyJointDef].ForEach (item =>
			{
				item.SetActive (false);
			});
		}
		if (DictMap.ContainsKey (RLegJointDef))
		{
			DictMap [RLegJointDef].ForEach (item =>
			{
				item.SetActive (false);
			});
		}

		if (DictMap.ContainsKey (LLegJointDef))
		{
			DictMap [LLegJointDef].ForEach (item =>
			{
				item.SetActive (false);
			});
		}
	}

	void Display_BodyJointDef ()
	{
		GameObject knob_Neck;
		GameObject knob_RLeg;
		GameObject knob_LLeg;

		if (DictMap.ContainsKey (BodyJointDef))
		{
			knob_Neck = DictMap [BodyJointDef] [0];
			knob_RLeg = DictMap [BodyJointDef] [1];
			knob_LLeg = DictMap [BodyJointDef] [2];
		}
		else
		{
			knob_Neck = Instantiate (KnobTemplate);
			knob_RLeg = Instantiate (KnobTemplate);
			knob_LLeg = Instantiate (KnobTemplate);

			DictMap.Add (BodyJointDef, new List<GameObject> ());
			DictMap [BodyJointDef].Add (knob_Neck);
			DictMap [BodyJointDef].Add (knob_RLeg);
			DictMap [BodyJointDef].Add (knob_LLeg);

			knob_Neck.transform.SetParent (transform);
			knob_RLeg.transform.SetParent (transform);
			knob_LLeg.transform.SetParent (transform);

			knob_Neck.transform.SetParent (transform);
			knob_Neck.transform.localPosition = Vector3.zero;
			knob_Neck.name = "Body-Neck";
			knob_Neck.GetComponent<JointKnob> ().Comment.text = "Body-Neck";

			knob_RLeg.transform.SetParent (transform);
			knob_RLeg.transform.localPosition = Vector3.zero;
			knob_RLeg.name = "Body-RLeg";
			knob_RLeg.GetComponent<JointKnob> ().Comment.text = "Body-RLeg";

			knob_LLeg.transform.SetParent (transform);
			knob_LLeg.transform.localPosition = Vector3.zero;
			knob_LLeg.name = "Body-LLeg";
			knob_LLeg.GetComponent<JointKnob> ().Comment.text = "Body-LLeg";
		}

		knob_Neck.SetActive (true);
		knob_RLeg.SetActive (true);
		knob_LLeg.SetActive (true);


		// Turn off
		if (DictMap.ContainsKey (HeadJointDef))
		{
			DictMap [HeadJointDef].ForEach (item =>
			{
				item.SetActive (false);
			});
		}

		if (DictMap.ContainsKey (RLegJointDef))
		{
			DictMap [RLegJointDef].ForEach (item =>
			{
				item.SetActive (false);
			});
		}

		if (DictMap.ContainsKey (LLegJointDef))
		{
			DictMap [LLegJointDef].ForEach (item =>
			{
				item.SetActive (false);
			});
		}
	}

	void Display_RLegJointDef ()
	{
		GameObject knob_Pelvis;

		if (DictMap.ContainsKey (RLegJointDef))
		{
			knob_Pelvis = DictMap [RLegJointDef] [0];
		}
		else
		{
			knob_Pelvis = Instantiate (KnobTemplate);

			DictMap.Add (RLegJointDef, new List<GameObject> ());
			DictMap [RLegJointDef].Add (knob_Pelvis);

			knob_Pelvis.transform.SetParent (transform);
			knob_Pelvis.transform.localPosition = Vector3.zero;
			knob_Pelvis.name = "RLeg-Pelvis";
			knob_Pelvis.GetComponent<JointKnob> ().Comment.text = "RLeg-Pelvis";
		}

		knob_Pelvis.SetActive (true);

		// Turn off
		if (DictMap.ContainsKey (HeadJointDef))
		{
			DictMap [HeadJointDef].ForEach (item =>
			{
				item.SetActive (false);
			});
		}

		if (DictMap.ContainsKey (BodyJointDef))
		{
			DictMap [BodyJointDef].ForEach (item =>
			{
				item.SetActive (false);
			});
		}

		if (DictMap.ContainsKey (LLegJointDef))
		{
			DictMap [LLegJointDef].ForEach (item =>
			{
				item.SetActive (false);
			});
		}
	}

	void Display_LLegJointDef ()
	{
		GameObject knob_Pelvis;
		if (DictMap.ContainsKey (LLegJointDef))
		{
			knob_Pelvis = DictMap [LLegJointDef] [0];
		}
		else
		{
			knob_Pelvis = Instantiate (KnobTemplate);

			DictMap.Add (LLegJointDef, new List<GameObject> ());
			DictMap [LLegJointDef].Add (knob_Pelvis);

			knob_Pelvis.transform.SetParent (transform);
			knob_Pelvis.transform.localPosition = Vector3.zero;
			knob_Pelvis.name = "LLeg-Pelvis";
			knob_Pelvis.GetComponent<JointKnob> ().Comment.text = "LLeg-Pelvis";
		}

		knob_Pelvis.SetActive (true);

		if (DictMap.ContainsKey (HeadJointDef))
		{
			DictMap [HeadJointDef].ForEach (item =>
			{
				item.SetActive (false);
			});
		}

		if (DictMap.ContainsKey (BodyJointDef))
		{
			DictMap [BodyJointDef].ForEach (item =>
			{
				item.SetActive (false);
			});
		}

		if (DictMap.ContainsKey (RLegJointDef))
		{
			DictMap [RLegJointDef].ForEach (item =>
			{
				item.SetActive (false);
			});
		}
	}
}
