// 

using System.Collections.Generic;
using UnityEngine;

namespace MyVisualNovel.Editor
{
	public interface IScriptableObjectConverter
	{
		public ScriptableObject ConvertFromFile(string path);
	}

	public interface IScriptableObjectBatchConverter
	{
		public List<ScriptableObject> ConvertFromFolder(string path);
	}

	public abstract class DialogueBatchConverter : IScriptableObjectBatchConverter
	{
		public DialogueBatchConverter() { }

		public virtual List<ScriptableObject> ConvertFromFolder(string path) { return null; }
	}
}
