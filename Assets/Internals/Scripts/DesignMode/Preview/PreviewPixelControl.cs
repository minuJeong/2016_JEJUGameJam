﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


[Serializable]
public abstract class PartJointDef
{
	
}

[Serializable]
public class HeadJointDef : PartJointDef
{
	public int[] Neck;
}

[Serializable]
public class BodyJointDef : PartJointDef
{
	public int[] Neck;
	public int[] RLegPelvis;
	public int[] LLegPelvis;
}

[Serializable]
public class RLegJointDef : PartJointDef
{
	public int[] Pelvis;
}

[Serializable]
public class LLegJointDef : PartJointDef
{
	public int[] Pelvis;
}


public class PreviewPixelControl : MonoBehaviour
{
	readonly static Dictionary<ChrPart, PreviewPixelControl> partToControl = new Dictionary<ChrPart, PreviewPixelControl> ();

	// Part
	public ChrPart Part;

	public PartJointDef JointDef;


	// Constants
	public const int X_COUNT = 25;

	public const int Y_COUNT = 25;


	const float m_GridSpacing = 0.5F;

	const float m_AlphaCurrentLayer = 1.0F;

	const float m_AlphaOtherLayer = 0.3F;


	// Protecteds
	protected Rect m_ClickArea;

	protected Image[,] m_Pixels = new Image[X_COUNT, Y_COUNT];

	protected Vector3 m_PreviousPosR;

	protected Texture2D m_Texture;


	public static void UpdatePart (ChrPart part)
	{
		var e = partToControl.GetEnumerator ();
		while (e.MoveNext ())
		{
			if (e.Current.Key == part)
			{
				e.Current.Value.transform.SetAsLastSibling ();

				foreach (var pixel in e.Current.Value.m_Pixels)
				{
					var c = pixel.color;
					c.a = m_AlphaCurrentLayer;
					pixel.color = c;

					pixel.raycastTarget = true;
				}
			}
			else
			{
				foreach (var pixel in e.Current.Value.m_Pixels)
				{
					var c = pixel.color;
					c.a = m_AlphaOtherLayer;
					pixel.color = c;

					pixel.raycastTarget = false;
				}
			}
		}
	}


	// Pixels
	public Image this [int x, int y]
	{
		get
		{
			if (!IsInRange (x, y))
			{
				Debug.LogError ("Should not access this");
				return null;
			}

			return m_Pixels [x, y];
		}

		set
		{
			if (!IsInRange (x, y))
			{
				Debug.LogError ("Should not access this");
				return;
			}

			m_Pixels [x, y] = value;
		}
	}

	static bool IsInRange (int x, int y)
	{
		bool result = true;
		if (x < 0 || x >= X_COUNT)
		{
			result = false;
		}

		if (y < 0 || y >= Y_COUNT)
		{
			result = false;
		}

		return result;
	}



	// Sprays images
	protected virtual void Awake ()
	{
		for (int y = Y_COUNT - 1; y >= 0; y--)
		{
			for (int x = 0; x < X_COUNT; x++)
			{
				this [x, y] = new GameObject ("Pixel", new []{ typeof(Image) }).GetComponent<Image> ();
				this [x, y].transform.SetParent (transform);
				this [x, y].transform.localScale = Vector3.one;
				this [x, y].raycastTarget = false;
				var c = this [x, y].color;
				c.a = m_AlphaOtherLayer;
				this [x, y].color = c;
			}
		}

		m_ClickArea = ((RectTransform)transform).rect;

		GetComponent<GridLayoutGroup> ().cellSize = new Vector2 ((m_ClickArea.width / X_COUNT) - m_GridSpacing, (m_ClickArea.width / Y_COUNT) - m_GridSpacing);
		GetComponent<GridLayoutGroup> ().spacing = new Vector2 (m_GridSpacing, m_GridSpacing);

		GetComponent<GridLayoutGroup> ().constraintCount = X_COUNT;

		ColorPicker.CurrentColor = Color.white;


		partToControl [Part] = this;

		switch (Part)
		{
		case ChrPart.Head:
			JointDef = new HeadJointDef ();
			break;

		case ChrPart.Body:
			JointDef = new BodyJointDef ();
			break;

		case ChrPart.RLeg:
			JointDef = new RLegJointDef ();
			break;

		case ChrPart.LLeg:
			JointDef = new LLegJointDef ();
			break;
		}
	}


	// On Click Handler
	public void OnClick (bool isDragging)
	{
		Vector3 clickPos = Input.mousePosition;

		clickPos.x -= transform.position.x;
		clickPos.y -= transform.position.y;

		Vector3 ratio = new Vector3 (clickPos.x / m_ClickArea.width, clickPos.y / m_ClickArea.height);

		if (isDragging)
		{
			StrokeTo (ratio);
		}
		else
		{
			Dot (ratio);
		}

		m_PreviousPosR = ratio;
	}

	void Dot (Vector3 ratio)
	{
		int x = (int)(ratio.x * X_COUNT);
		int y = (int)(ratio.y * Y_COUNT);

		if (IsInRange (x, y))
		{
			this [x, y].color = ColorPicker.CurrentColor;
		}
	}

	void StrokeTo (Vector3 ratio)
	{
		const float SPACING = 25;

		for (int i = 0; i < SPACING; i++)
		{
			Dot (Vector3.Lerp (m_PreviousPosR, ratio, i / SPACING));
		}
	}

	public void OnClickSave ()
	{
	}
}