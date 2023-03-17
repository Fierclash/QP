// #DataStructScript

using UnityEngine;
using VisualNovelEngine.Core;

namespace VisualNovelEngine.Dialogue
{
	/// <summary>
	/// Data struct for dialogue data.
	/// </summary>
	[System.Serializable]
	public class DialogueData : TextUIData, IKeyAttribute, IDialogueAttribute, ISpeakerAttribute,
								IExpressionAttribute
	{
		[SerializeField]
		private string _route;
		public string Route
		{
			get { return _route; }
			set { _route = value; }
		}

		[SerializeField]
		private string _key;
		public string Key
		{
			get { return _key; }
			set { _key = value; }
		}

		[SerializeField]
		private string _speaker;
		public string Speaker
		{
			get { return _speaker; }
			set { _speaker = value; }
		}

		[SerializeField]
		private string _dialogue;
		public string Dialogue
		{
			get { return _dialogue; }
			set { _dialogue = value; }
		}

		[SerializeField]
		private string _expression;
		public string Expression
		{
			get { return _expression; }
			set { _expression = value; }
		}
	}
}
