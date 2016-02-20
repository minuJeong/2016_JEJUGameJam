using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatPoint : MonoBehaviour
{
	public static StatPoint Instance;

	public int ALL_STAT_SUM = 20;

	public int HEALTH = 0;
	public int ATTACK = 0;
	public int DEFFENCE = 0;
	public int MAGIC = 0;
	public int SOCIAL = 0;

	public int STAT_SUM
	{
		get
		{
			return HEALTH + ATTACK + DEFFENCE + MAGIC + SOCIAL;
		}
	}

	public bool IsStatSafe
	{
		get
		{
			return STAT_SUM <= ALL_STAT_SUM;
		}
	}

	public Text HealthValueText;
	public Text AttackValueText;
	public Text DeffenceValueText;
	public Text MagicValueText;
	public Text SocialValueText;

	public Text RemainingPointText;

	public Button ConfirmButton;

	void Awake ()
	{
		Instance = this;
	}

	void Start ()
	{
		UpdateText ();
	}

	void UpdateText ()
	{
		HealthValueText.text = HEALTH.ToString ();
		AttackValueText.text = ATTACK.ToString ();
		DeffenceValueText.text = DEFFENCE.ToString ();
		MagicValueText.text = MAGIC.ToString ();
		SocialValueText.text = SOCIAL.ToString ();

		RemainingPointText.text = (IsStatSafe ? "+ " : "- ") + Mathf.Abs(ALL_STAT_SUM - STAT_SUM);

		if (IsStatSafe)
		{
			RemainingPointText.color = Color.black;
		}
		else
		{
			RemainingPointText.color = Color.red;
		}
	}

	public void AddHealth (int adjust)
	{
		HEALTH += adjust;

		HEALTH = Mathf.Max (HEALTH, 1);

		UpdateText ();
	}

	public void AddAttack (int adjust)
	{
		ATTACK += adjust;

		ATTACK = Mathf.Max (ATTACK, 1);

		UpdateText ();
	}

	public void AddDeffence (int adjust)
	{
		DEFFENCE += adjust;

		DEFFENCE = Mathf.Max (DEFFENCE, 0);

		UpdateText ();
	}

	public void AddMagic (int adjust)
	{
		MAGIC += adjust;

		MAGIC = Mathf.Max (MAGIC, 0);

		UpdateText ();
	}

	public void AddSocial (int adjust)
	{
		SOCIAL += adjust;

		SOCIAL = Mathf.Max (SOCIAL, 0);

		UpdateText ();
	}
}