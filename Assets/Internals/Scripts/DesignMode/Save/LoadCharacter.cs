using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class LoadCharacter
{
	public static SaveModel[] LoadData ()
	{
		string[] files = Directory.GetFiles (Application.persistentDataPath + SaveCharData.SAVE_DATA_PATH_FIX);

		int len = files.Length;

		List<SaveModel> models = new List<SaveModel> ();

		for (int i = 0; i < len; i++)
		{
			var file = files [i];
			
			FileStream FStream = File.Open (file, FileMode.Open);

			if (file.Contains (".DS_Store"))
			{
				continue;
			}

			BinaryFormatter BF = new BinaryFormatter ();

			var deserialized = (SaveModel)BF.Deserialize (FStream);

			if (deserialized == null)
			{
				continue;
			}

			models.Add (deserialized);
		}

		return models.ToArray ();
	}
}
