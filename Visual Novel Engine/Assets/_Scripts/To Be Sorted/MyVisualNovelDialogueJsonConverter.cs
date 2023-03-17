// #LogicScript

// Contains a specific implementation to a specific .json structure.
// Switch DialogueConverter if a different implemenation is needed.

using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using VisualNovelEngine.Dialogue;
using UnityEditor;
using VisualNovelEngine.Core;

namespace MyVisualNovel.Editor
{
	public class DialogueJson
	{
		public List<DialogueData> data = new List<DialogueData>();
	}

	public class DialogueData
	{
		public int id;
		public string route;
		public string speakerName;
		public string sprite;
		public string text;
		public string choices;
	}

	public class MyVisualNovelDialogueJsonConverter : DialogueBatchConverter
	{
		public List<ScriptableObject> GenerateKeyData(string path)
		{

			string[] files = Directory.GetFiles(path, "*_data.json", SearchOption.TopDirectoryOnly);
			List<ScriptableObject> sos = new List<ScriptableObject>();
			foreach (string file in files)
			{
				Debug.Log(file);
				StreamReader streamReader = new StreamReader(file);
				string json = streamReader.ReadToEnd();
				streamReader.Close();

				Debug.Log("here");
				DialogueJson dialogueJson = JsonConvert.DeserializeObject<DialogueJson>(json);
				Debug.Log("now here");

				if (dialogueJson == null)
				{
					Debug.LogWarning("Json is null.");
					continue;
				}

				SceneEventKeyListSO sceneKeysSO = ScriptableObject.CreateInstance<SceneEventKeyListSO>();
				sceneKeysSO.Value = new SceneEventKeyList();
				sceneKeysSO.name = $"{Path.GetFileNameWithoutExtension(file)}";
				for (int i = 0; i < dialogueJson.data.Count; i++)
				{
					DialogueData data = dialogueJson.data[i];
					sceneKeysSO.Value.Keys.Add(string.Join("_", data.id, string.IsNullOrEmpty(data.route) ? "O" : data.route));
				}

				
				//so.Value.Keys = dialogueJson.data.Select(x => string.Join("_", x.id, string.IsNullOrEmpty(x.route) ? "O" : x.route))
				//							.Distinct()
				//							.ToList();

				sos.Add(sceneKeysSO);
			}
			return sos;
		}

		public override List<ScriptableObject> ConvertFromFolder(string path)
		{
			string[] files = Directory.GetFiles(path, "*_data.json", SearchOption.TopDirectoryOnly);
			List<ScriptableObject> sos = new List<ScriptableObject>();
			foreach (string file in files)
			{
				StreamReader streamReader = new StreamReader(file);
				string json = streamReader.ReadToEnd();
				streamReader.Close();

				//Debug.Log(json);
				DialogueJson dialogueJson = JsonConvert.DeserializeObject<DialogueJson>(json);

				for (int i = 0; i < dialogueJson.data.Count; i++)
				{
					DialogueData data = dialogueJson.data[i];

					DialogueSO dialogueSO = ScriptableObject.CreateInstance<DialogueSO>();
					dialogueSO.Value = new VisualNovelEngine.Dialogue.DialogueData()
					{
						Key = string.Join("_", data.id, string.IsNullOrEmpty(data.route) ? "O" : data.route),
						Speaker = data.speakerName,
						Dialogue = data.text,
						Expression = data.sprite
					};

					dialogueSO.name = $"{Path.GetFileNameWithoutExtension(file)}_{i}";
					//Debug.Log(dialogueSO.name);

					sos.Add(dialogueSO);
				}
			}

			return sos;
		}
	}
}
