using UnityEngine;
using System;
using System.Collections.Generic;
using Vectrosity;

public class GraphRenderer : MonoBehaviour
{
	public static GraphRenderer Instance;


	public static void Draw ()
	{
		int len = Instance.positions.Count;
		var lines = new List<VectorLine> ();

		for (int i = len - 1; i > 0; i--)
		{
			lines.Add (new VectorLine (string.Format ("{0} ~ {1}", i, i - 1), new List<Vector2> {
				Instance.positions [i], Instance.positions [i - 1]
			}, 1.0F, LineType.Continuous, Joins.Weld));
		}

		lines.Add (new VectorLine (string.Format ("0 ~ {0}", len - 1), new List<Vector2> {
			Instance.positions [0], Instance.positions [len - 1]
		}, 1.0F, LineType.Continuous, Joins.Weld));

		lines.ForEach (line =>
		{
			line.color = new Color (1.0F, 0.0F, 0.0F);

			Debug.Log ("====");
			Debug.Log (line.GetPoint (0));
			Debug.Log (line.GetPoint (1));

			line.Draw ();
		});
	}


	public GameObject HealthPos;
	public GameObject AttackPos;
	public GameObject DefencePos;
	public GameObject MagicPos;
	public GameObject SocialPos;

	protected List<Vector2> positions;

	void Start ()
	{
		Instance = this;

		positions = new List<Vector2> {
			HealthPos.transform.position,
			AttackPos.transform.position,
			DefencePos.transform.position,
			MagicPos.transform.position,
			SocialPos.transform.position
		};

		Debug.Log (HealthPos.transform.position);

		Draw ();
	}

	void Update ()
	{
		
	}
}
