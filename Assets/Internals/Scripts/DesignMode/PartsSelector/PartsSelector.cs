using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class PartsSelector : MonoBehaviour
{
	public static PartsSelector Instance;

	public PartControl[] m_Controls;

	public PartControl m_CurrentPart;

	[SerializeField]
	Color m_OnSelect;

	[SerializeField]
	Color m_OnDeselect;

	void Awake ()
	{
		Instance = this;
	}


	void Start ()
	{
		Debug.Assert (m_Controls.Length == (int)ChrPart.COUNT, "all ChrParts are not serialized");

		m_CurrentPart = m_Controls [0];

		foreach (var control in m_Controls)
		{
			if (m_CurrentPart.Part == control.Part)
			{
				control.ControllerButton.GetComponent<Image> ().color = m_OnSelect;
			}
			else
			{
				control.ControllerButton.GetComponent<Image> ().color = m_OnDeselect;
			}
		}
	}

	public void SelectPart (string part)
	{
		foreach (var control in m_Controls)
		{
			if (control.Part.ToString () == part)
			{
				m_CurrentPart = control;

				control.ControllerButton.GetComponent<Image> ().color = m_OnSelect;
			}
			else
			{
				control.ControllerButton.GetComponent<Image> ().color = m_OnDeselect;
			}
		}


		ChrPart setPart = (ChrPart)System.Enum.Parse (typeof(ChrPart), part);
		PreviewPixelControl.UpdatePart (setPart);
		JointDisplay.ChangeJointDisplayDef (setPart);
	}
}


public enum ChrPart
{
	Head,
	Body,
	RLeg,
	LLeg,
	COUNT
}

[System.Serializable]
public class PartControl
{
	public string Flag;

	public GameObject ControllerButton;

	public ChrPart Part;
}


#if UNITY_EDITOR
[CustomPropertyDrawer (typeof(PartControl))]
public class PartControlEditor : PropertyDrawer
{
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		// base.OnGUI (position, property, label);

		// Draw label
		label = new GUIContent ("Part");

		position = EditorGUI.PrefixLabel (new Rect (position.x, position.y, position.width, position.height + 30), GUIUtility.GetControlID (FocusType.Passive), label);

		EditorGUI.BeginProperty (position, label, property);

		int prevIndent = EditorGUI.indentLevel;

		Rect buttonRect = new Rect (position.x, position.y, 100, 15);

		Rect partRect = new Rect (position.x + 100, position.y, 100, 15);

		EditorGUI.PropertyField (buttonRect, property.FindPropertyRelative ("ControllerButton"), GUIContent.none);

		EditorGUI.PropertyField (partRect, property.FindPropertyRelative ("Part"), GUIContent.none);

		EditorGUI.indentLevel = prevIndent;

		EditorGUI.EndProperty ();
	}
}
#endif