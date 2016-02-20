using UnityEngine;
using System.Collections;

public class JointDisplay : MonoBehaviour
{
	public static Transform JointSelecterParent;

	void Awake ()
	{
		JointSelecterParent = transform;
	}

	public static void ShowJoint (PartJointDef def)
	{
		if (def is HeadJointDef)
		{
			Display_HeadJointDef ();
		}
		else
		if (def is BodyJointDef)
		{
			
		}
		else
		if (def is RLegJointDef)
		{
			
		}
		else
		if (def is LLegJointDef)
		{
			
		}
	}


	void Display_HeadJointDef ()
	{
		Debug.Log ("DDD");
	}
}
