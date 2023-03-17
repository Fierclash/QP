// 

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace MyVisualNovel.Editor
{
	public class DialogueDataGenerator
	{
		private class Metadata
		{
			public string routeKey;
			public string setting;
		}

		private class Data
		{
			public string speakerName;
			public string sprite;
			public string text;
		}

		public void ImportJson()
		{
			string path = "CSV/DialogueData/Dialogue_A_data.json";
			StreamReader reader = new StreamReader(path);
			string json = reader.ReadToEnd();
			reader.Close();
			//Metadata meta = JsonUtility.FromJson<Metadata>(json);
			List<Data> data = JsonUtility.FromJson<List<Data>>(json);

			Debug.Log(string.Join('\n', data.Select(x => $"{x.speakerName}:{x.sprite}:{x.text}")));
		}
	}
}
