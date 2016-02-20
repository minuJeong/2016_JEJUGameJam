using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorPicker : MonoBehaviour
{
	public static Color CurrentColor;

	public RawImage HuePicker;

	public RawImage PallettePicker;

	public Image SelectedColor;

	public PreviewPixelControl PreviewPixelControl;


	HSBColor m_SelectingColor = new HSBColor (0, 0, 0);

	void Start ()
	{
		OnClickPallette ();

		UpdateColor ();
	}

	void UpdateColor ()
	{
		SelectedColor.color = m_SelectingColor.ToColor ();

		PallettePicker.material.SetFloat ("_Hue", m_SelectingColor.h);

		CurrentColor = SelectedColor.color;
	}

	public void OnClickHue ()
	{
		m_SelectingColor.h = Mathf.Clamp01 ((Input.mousePosition.x - HuePicker.transform.position.x) / ((RectTransform)HuePicker.transform).sizeDelta.x);

		PallettePicker.material.SetFloat ("_Hue", m_SelectingColor.h);

		UpdateColor ();
	}

	public void OnClickPallette ()
	{
		m_SelectingColor.s = Mathf.Clamp01 ((Input.mousePosition.x - PallettePicker.transform.position.x) / ((RectTransform)PallettePicker.transform).sizeDelta.x);
		m_SelectingColor.b = 1.0F - Mathf.Clamp01 ((PallettePicker.transform.position.y - Input.mousePosition.y) / ((RectTransform)PallettePicker.transform).sizeDelta.y);

		Vector3 CursorPos = Input.mousePosition;

		CursorPos.x = Mathf.Clamp (CursorPos.x, PallettePicker.transform.position.x,
			PallettePicker.transform.position.x + ((RectTransform)PallettePicker.transform).sizeDelta.x);

		CursorPos.y = Mathf.Clamp (CursorPos.y, PallettePicker.transform.position.y - ((RectTransform)PallettePicker.transform).sizeDelta.y,
			PallettePicker.transform.position.y);

		SelectedColor.transform.position = CursorPos;

		UpdateColor ();
	}
}
