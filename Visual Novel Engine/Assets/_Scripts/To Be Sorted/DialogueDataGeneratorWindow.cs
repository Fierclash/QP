// 

#if UNITY_EDITOR
using System.Linq;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using VisualNovelEngine.Dialogue;
using System;
using System.Reflection;
#endif

namespace MyVisualNovel.Editor
{
#if UNITY_EDITOR
	public class DialogueDataGeneratorWindow : OdinEditorWindow
	{
		public class EditorDialogue
		{
			[InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
			public DialogueSO data;

			public EditorDialogue()
			{

			}
		}

		[TitleGroup("Dialogue Generation")]
		[BoxGroup("Dialogue Generation/General")]
		[ValueDropdown("ConversionMethods")]
		public string conversionMethod;

		[TitleGroup("Dialogue Generation")]
		[BoxGroup("Dialogue Generation/Group Generation")]
		[FolderPath]
		public string path;

		private string[] ConversionMethods
		{
			get
			{
				var assembly = Assembly.GetExecutingAssembly();
				Type baseType = typeof(DialogueBatchConverter);
				var conversionMethods = assembly.GetTypes()
						.Where(baseType.IsAssignableFrom)
						.Where(t => baseType != t)
						.Select(t => t.Name)
						.ToArray();
				return conversionMethods;
			}
		}

		[MenuItem("Visual Novel Engine/Dialogue Generator")]
		public static void OpenWindow()
		{
			GetWindow<DialogueDataGeneratorWindow>().Show();
		}

		[BoxGroup("Dialogue Generation/Group Generation")]
		[ButtonGroup("Dialogue Generation/Group Generation/Generation")]
		[Button("Generate")]
		public void GenerateDialogueData()
		{
			try
			{
				//Debug.Log($"Finding {conversionMethod}");
				DialogueBatchConverter converter = DialogueGenerationManager.GetConverter(conversionMethod);
				var sos = converter.ConvertFromFolder(path);

				AssetDatabase.Refresh();
				foreach (var s in sos)
				{
					AssetDatabase.CreateAsset(s, $"Assets/Resources/Dialogue Data/{s.name}.asset");
					AssetDatabase.SaveAssets();
				}
				AssetDatabase.Refresh();

				Debug.Log("Successfully generated dialogue data.");
			}
			catch
			{
				throw new ArgumentException("Could not generate dialogue with current method.", conversionMethod);
			}
		}


		[TitleGroup("Dialogue Generation")]
		[BoxGroup("Dialogue Generation/Full Generation")]
		[Sirenix.OdinInspector.FilePath]
		public string fullPath;

		[BoxGroup("Dialogue Generation/Full Generation")]
		[ButtonGroup("Dialogue Generation/Full Generation/Generation")]
		[Button("Full")]
		public void GenerateSceneEventData()
		{
			//Debug.Log($"Finding {conversionMethod}");
			// #RQA
			MyVisualNovelDialogueJsonConverter converter = new MyVisualNovelDialogueJsonConverter();
			var sos = converter.ConvertFromFolder(path);

			AssetDatabase.Refresh();
			foreach (var s in sos)
			{
				AssetDatabase.CreateAsset(s, $"Assets/Resources/Dialogue Data/{s.name}.asset");
			}

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			Debug.Log("Successfully generated dialogue data.");
		}

		[BoxGroup("Dialogue Generation/Full Generation")]
		[ButtonGroup("Dialogue Generation/Full Generation/Generation")]
		[Button("Key Gen")]
		public void GenerateSceneKeyData()
		{
			//Debug.Log($"Finding {conversionMethod}");
			// #RQA
			MyVisualNovelDialogueJsonConverter converter = new MyVisualNovelDialogueJsonConverter();
			var sos = converter.ConvertFromFolder(path);

			var keySO = converter.GenerateKeyData(path);
			foreach (var s in keySO)
			{
				AssetDatabase.CreateAsset(s, $"Assets/Resources/Dialogue Data/{s.name}_SubKeys.asset");
			}
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			Debug.Log("Successfully generated dialogue data.");
		}
	}
#endif
}
