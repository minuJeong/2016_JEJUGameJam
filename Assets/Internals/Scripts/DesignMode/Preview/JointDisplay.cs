using UnityEngine;
using System.Collections;

public class JointDisplay : MonoBehaviour
{
	public static Transform JointSelecterParent;

	void Awake ()
	{
		JointSelecterParent = transform;
	}

	public static void ChangeJointDisplayDef (ChrPart part)
	{
		if (part == ChrPart.Head)
		{
			Display_HeadJointDef ();
		}
		else
		if (part == ChrPart.Body)
		{
			Display_BodyJointDef ();
		}
		else
		if (part == ChrPart.RLeg)
		{
			Display_RLegJointDef ();
		}
		else
		if (part == ChrPart.LLeg)
		{
			Display_LLegJointDef ();
		}
	}


	static void Display_HeadJointDef ()
	{
		Debug.Log ("Head");
	}

	static void Display_BodyJointDef ()
	{
		Debug.Log ("Body");
	}

	static void Display_RLegJointDef ()
	{
		Debug.Log ("RLeg");
	}

	static void Display_LLegJointDef ()
	{
		Debug.Log ("LLeg");
	}
}
