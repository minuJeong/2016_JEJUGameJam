using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveCharData
{
	public const string SAVE_DATA_PATH_FIX = "/svd/";


	public static void SaveData (SaveModel data)
	{
		string fileName = data.ID + data.SaveName;

		BinaryFormatter BF = new BinaryFormatter ();

		if (!Directory.Exists (Application.persistentDataPath + SAVE_DATA_PATH_FIX))
		{
			Directory.CreateDirectory (Application.persistentDataPath + SAVE_DATA_PATH_FIX);
		}

		FileStream FStream = File.Create (Application.persistentDataPath + SAVE_DATA_PATH_FIX + fileName);
		BF.Serialize (FStream, data);

		FStream.Close ();
	}

	public static SaveModel GetModel ()
	{
		SaveModel model = new SaveModel ();

		model.SaveName = "PlayerName";

		return model;
	}
}