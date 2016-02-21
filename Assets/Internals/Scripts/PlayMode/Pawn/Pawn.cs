using UnityEngine;
using System.Collections;


public enum State
{
	IDLE,
	WALK,
	ATTACK
}

public class Pawn : MonoBehaviour
{
	public State m_State = State.IDLE;

	// Use this for initialization
	void Start ()
	{
		
	}
}
