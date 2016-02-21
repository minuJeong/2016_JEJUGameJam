using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JointKnob : MonoBehaviour
{
	public Text Comment;

	public RectTransform ValidRectTransform;

	Rect ValidRect;

	public float xRatio;
	public float yRatio;

	public bool IsDragging = false;


	void Start ()
	{
		ValidRect = ValidRectTransform.rect;
		ValidRect.x += ValidRectTransform.position.x;
		ValidRect.y += ValidRectTransform.position.y;

		GetComponent<Image> ().color = Color.red;
	}

	public void UpdatePosition ()
	{
		GetComponent<Image> ().color = Color.yellow;

		UpdatePos ();
	}

	void UpdatePos ()
	{
		Vector3 tempPos = Input.mousePosition;

		tempPos.x = Mathf.Clamp (tempPos.x, ValidRect.xMin, ValidRect.xMax);
		tempPos.y = Mathf.Clamp (tempPos.y, ValidRect.yMin, ValidRect.yMax);

		transform.position = tempPos;

		xRatio = (tempPos.x - ValidRect.xMin) / (ValidRect.xMax - ValidRect.xMin);
		yRatio = (tempPos.y - ValidRect.yMin) / (ValidRect.yMax - ValidRect.yMin);

		JointDisplay.Instance.OnUpdateJointKnobPosition ();
	}
}
