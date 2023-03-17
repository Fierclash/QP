// #DataStructScript

using System.Collections.Generic;
using UnityEngine;
using VisualNovelEngine.Core;

namespace VisualNovelEngine.Dialogue
{
	[System.Serializable]
	public class ChoiceData : TextUIData, IKeyAttribute
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
		private List<string> _choices;
		public List<string> Choices
		{
			get { return _choices; }
			set { _choices = value; }
		}
	}
}
