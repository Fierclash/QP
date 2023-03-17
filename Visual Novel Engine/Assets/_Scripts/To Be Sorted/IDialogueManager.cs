// #FrameworkScript

using System.Collections.Generic;

namespace VisualNovelEngine.SceneEvents
{
	/// <summary>
	/// Base class for managing runtime dialogue data.
	/// </summary>
	public interface IDialogueManager
	{
		public Dictionary<string, DialogueData> Data
		{
			get;
		}
	}
}
