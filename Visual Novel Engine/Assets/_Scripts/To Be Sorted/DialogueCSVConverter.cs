// 

using System.Collections.Generic;
using UnityEngine;

namespace MyVisualNovel.Editor
{
	public class DialogueCSVConverter : DialogueBatchConverter
	{
		public override List<ScriptableObject> ConvertFromFolder(string filename)
		{
			return null;
		}
	}

	public class DialogueXMLConverter : DialogueBatchConverter { }
}
