
using UnityEngine;
using Sirenix.OdinInspector;
using VisualNovelEngine.Dialogue;
using MyVisualNovel.Dialogue;
using System.Linq;
using System.Collections.Generic;

namespace MyVisualNovel.Debugging
{
	public class DialogueWriterDebugger : MonoBehaviour
	{
		[TitleGroup("Components")]
		[SerializeField]
		private BasicDialogueWritingSystem writer;

		public string speaker;
		public string dialogue;

		[TitleGroup("Debug")]
		[ButtonGroup("Debug/Writer")]
		[Button("Write")]
		public void TestWrite()
		{
			DialogueData data = new DialogueData()
			{
				Speaker = speaker,
				Dialogue = dialogue
			};

			writer.WriteDialogue(data);
		}
	}
}
