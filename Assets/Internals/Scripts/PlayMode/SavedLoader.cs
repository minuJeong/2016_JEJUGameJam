using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class SavedLoader : MonoBehaviour
{
	public static SavedLoader Instance;

	readonly public static Dictionary<int, SaveModel> LoadedIndexToModelMap = new Dictionary<int, SaveModel> ();

	public Dropdown DropDownMenu;

	public SaveModel SelectedModel
	{
		get
		{
			int index = DropDownMenu.value;

			if (!LoadedIndexToModelMap.ContainsKey (index))
			{
				Reload ();

				if (!LoadedIndexToModelMap.ContainsKey (index))
				{
					Debug.LogError ("No Model Found.");
				}

				return null;
			}

			return LoadedIndexToModelMap [index];
		}
	}

	void Awake ()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		Reload ();
	}

	public void Reload ()
	{
		DropDownMenu.ClearOptions ();

		SaveModel[] models = LoadCharacter.LoadData ();

		List<Dropdown.OptionData> optionDataSet = new List<Dropdown.OptionData> ();

		int len = models.Length;

		for (int i = 0; i < len; i++)
		{
			var model = models [i];

			LoadedIndexToModelMap [i] = model;

			Dropdown.OptionData data = new Dropdown.OptionData (model.SaveName);

			optionDataSet.Add (data);
		}

		DropDownMenu.AddOptions (optionDataSet);
	}

	public SaveModel PickModel ()
	{
		SaveModel[] models = LoadCharacter.LoadData ();
		return models [UnityEngine.Random.Range (0, models.Length)];
	}
}
