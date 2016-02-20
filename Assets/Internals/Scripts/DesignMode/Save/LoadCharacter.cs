using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class LoadCharacter
{
	public static SaveModel[] LoadData ()
	{
		string[] files = Directory.GetFiles (Application.persistentDataPath + SaveCharData.SAVE_DATA_PATH_FIX);

		int len = files.Length;

		SaveModel[] model = new SaveModel[len];

		for (int i = 0; i < len; i++)
		{
			var file = files [i];
			
			FileStream FStream = File.Open (file, FileMode.Open);

			if (file.Contains (".DS_Store"))
			{
				continue;
			}

			BinaryFormatter BF = new BinaryFormatter ();

			model [i] = (SaveModel)BF.Deserialize (FStream);

			Debug.Log ("after deserialize");
		}

		return model;
	}
}
