using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;


public class Pawn : MonoBehaviour
{
	readonly public static List<Pawn> Pawns = new List<Pawn> ();

	public Pawn Target = null;

	public Vector3 TargetPos;

	public bool IsDead = false;

	public float AttackRange = 3.0F;

	public float MoveSpeed = 0.3F;


	public float m_HP = 100.0F;

	public float HP
	{
		get
		{
			return m_HP;
		}

		set
		{
			m_HP = value;

			if (m_HP <= 0)
			{
				m_HP = 0.0F;

				IsDead = true;

				Die ();
			}
		}
	}

	public float ATTACK = 10.0F;



	GameObject LightSabre;


	// Use this for initialization
	void Start ()
	{
		TargetPos = transform.position;

		if (null != PawnSpawner.Instance.LightSabrePrefab)
		{
			LightSabre = Instantiate (PawnSpawner.Instance.LightSabrePrefab);
			LightSabre.transform.SetParent (transform);
			LightSabre.transform.localPosition = new Vector3 (-0.08F, 0.0F);
			LightSabre.transform.localRotation = Quaternion.Euler (0, 0, 38.0F);
		}

		StartCoroutine (Think ());
	}

	void Update ()
	{
		if (IsDead)
		{
			return;
		}

		Vector3 delta = (TargetPos - transform.position);

		Vector3 temp = transform.localScale;
		if (delta.x > 0)
		{
			temp.x = -Mathf.Abs (transform.localScale.x);
		}

		if (delta.x < 0)
		{
			temp.x = Mathf.Abs (transform.localScale.x);
		}
		transform.localScale = temp;

		transform.position -= delta * (MoveSpeed * Time.deltaTime);
	}


	Pawn PickPawn ()
	{
		int indx = Random.Range (0, Pawns.Count);
		if (Pawns [indx] == this)
		{
			return PickPawn ();
		}

		return Pawns [indx];
	}

	IEnumerator Think ()
	{
		while (true)
		{
			if (Pawns.Count >= 3)
			{
				if (null == Target || Target.IsDead)
				{
					Target = PickPawn ();
				}
				if (null != Target)
				{
					Vector3 delta = (transform.position - Target.transform.position);
					if (delta.magnitude > AttackRange)
					{
						WalkToTarget ();
					}
					else
					{
						AttackTarget ();
					}	
				}
			}

			yield return new WaitForSeconds (0.25F);
		}
	}

	void WalkToTarget ()
	{
		if (null != PawnSpawner.Instance.Walk)
		{
			if (GetComponent<Animation> ().GetClip ("Walk") == null)
			{
				GetComponent<Animation> ().AddClip (PawnSpawner.Instance.Walk, "Walk");
			}
			GetComponent<Animation> ().Play ("Walk");
		}

		Vector3 delta = Target.transform.position - transform.position;
		TargetPos = Target.transform.position - (delta * AttackRange);
	}

	void AttackTarget ()
	{
		if (null != PawnSpawner.Instance.Attack)
		{
			if (GetComponent<Animation> ().GetClip ("Attack") == null)
			{
				GetComponent<Animation> ().AddClip (PawnSpawner.Instance.Attack, "Attack");
			}
			GetComponent<Animation> ().Play ("Attack");
		}

		LightSabre.transform.DORotate (new Vector3 (0, 0, Random.Range (-120, 120)), 0.2F, RotateMode.Fast).SetRelative ();

		TargetPos = transform.position;

		Target.HP -= ATTACK;

		if (Target.IsDead)
		{
			Target = null;
		}
	}

	void Die ()
	{
		StopCoroutine ("Think");

		transform.DOScaleY (0, 1.0F).OnComplete (() =>
		{
			Destroy (gameObject);
		});

		transform.DOMoveY (-2.0F, 1.0F);

		Pawns.Remove (this);
	}
}
