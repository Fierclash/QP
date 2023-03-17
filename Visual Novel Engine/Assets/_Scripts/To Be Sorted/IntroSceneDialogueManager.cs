using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VisualNovelEngine.SceneEvents;

namespace MyVisualNovel
{
	public class IntroSceneDialogueManager : MonoBehaviour, IDialogueManager
	{
		private Dictionary<string, DialogueData> dialogueData;

		public Dictionary<string, DialogueData> Data
		{
			get { return dialogueData; }
		}
	}
}
