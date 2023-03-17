// #FrameworkScript

using UnityEngine;
using VisualNovelEngine.Dialogue;

namespace MyVisualNovel.Dialogue
{
	public abstract class DialogueWritingSystem : MonoBehaviour
	{
		public virtual void WriteDialogue(TextUIData data) { }
	}
}
