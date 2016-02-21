using UnityEngine;
using UnityEngine.UI;



public enum DesignDetailMode
{
	DrawingMode,
	JointMode
}

public class DesignModeSelector : MonoBehaviour
{
	public static DesignModeSelector Instance;

	public Button DrawingMode;

	public Button JointMode;

	public Image Checker;


	protected DesignDetailMode m_DMode;

	public DesignDetailMode DMode
	{
		get
		{
			return m_DMode;
		}

		set
		{
			m_DMode = value;

			switch (value)
			{
			case DesignDetailMode.DrawingMode:
				Checker.transform.position = DrawingMode.transform.position;
				break;

			case DesignDetailMode.JointMode:
				Checker.transform.position = JointMode.transform.position;
				break;
			}

			JointDisplay.ChangeJointDisplayDef (PartsSelector.Instance.m_CurrentPart.Part);
		}
	}


	void Awake ()
	{
		Instance = this;
	}


	public void SelectDrawingMode ()
	{
		DMode = DesignDetailMode.DrawingMode;
	}

	public void SelectJointMode ()
	{
		DMode = DesignDetailMode.JointMode;
	}
}
