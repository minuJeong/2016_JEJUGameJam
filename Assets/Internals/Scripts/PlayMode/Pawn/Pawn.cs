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


	public AnimationClip Idle;

	public AnimationClip Walk;

	public AnimationClip Attack;


	// Use this for initialization
	void Start ()
	{
		TargetPos = transform.position;

		StartCoroutine (Think ());
	}

	void Update ()
	{
		if (IsDead)
		{
			return;
		}

		transform.position -= (TargetPos - transform.position) * (MoveSpeed * Time.deltaTime);
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
		if (null != Walk)
		{
			if (GetComponent<Animation> ().GetClip ("Walk") == null)
			{
				GetComponent<Animation> ().AddClip (Walk, "Walk");
			}
			GetComponent<Animation> ().Play ("Walk");
		}

		Vector3 delta = Target.transform.position - transform.position;
		TargetPos = Target.transform.position - (delta * AttackRange);
	}

	void AttackTarget ()
	{
		if (null != Attack)
		{
			if (GetComponent<Animation> ().GetClip ("Attack") == null)
			{
				GetComponent<Animation> ().AddClip (Attack, "Attack");
			}
			GetComponent<Animation> ().Play ("Attack");
		}

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
