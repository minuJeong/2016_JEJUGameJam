using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToastMessage : MonoBehaviour
{
	float alphaTo;

	[SerializeField]
	protected Text m_MessageText;

	[SerializeField]
	protected Image m_MessageBox;

	public void ShowMessage (string message)
	{
		m_MessageText.text = message;

		alphaTo = 1.0F;

		StopCoroutine ("WaitAndFade");
		StartCoroutine ("WaitAndFade");
	}

	IEnumerator WaitAndFade ()
	{
		yield return new WaitForSeconds (1.5F);

		alphaTo = 0.0F;
	}

	// Update is called once per frame
	void Update ()
	{
		var tempBx = m_MessageBox.color;
		tempBx.a += (alphaTo - m_MessageBox.color.a) * 0.1F;
		m_MessageBox.color = tempBx;

		var tempTxt = m_MessageText.color;
		tempTxt.a += (alphaTo - m_MessageText.color.a) * 0.1F;
		m_MessageText.color = tempTxt;

	}
}
