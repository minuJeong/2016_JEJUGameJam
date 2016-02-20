using UnityEngine;
using System.Collections;

public class StatPoint : MonoBehaviour
{
	public static StatPoint Instance;

	public int HEALTH = 0;

	public int ATTACK = 0;

	public int DEFFENCE = 0;

	public int MAGIC = 0;

	public int SOCIAL = 0;


	void Awake ()
	{
		Instance = this;
	}
}