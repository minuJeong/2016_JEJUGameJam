using UnityEngine;

public class SaveName : MonoBehaviour
{
	public static SaveName Instance;

	protected string m_SetName = "No Name";

	bool m_AlreadySetName = false;

	public string SetName
	{
		get
		{
			return m_SetName;
		}

		set
		{
			if (m_AlreadySetName)
			{
				return;
			}

			m_SetName = value;

			m_AlreadySetName = true;
		}
	}

	void Awake ()
	{
		Instance = this;
	}
}
