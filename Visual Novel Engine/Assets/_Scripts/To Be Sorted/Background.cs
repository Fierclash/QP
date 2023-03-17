// #DataStructScript

using UnityEngine;
using VisualNovelEngine.Core;

namespace VisualNovelEngine.Environment
{
	[System.Serializable]
	public class Background : IKeyAttribute
	{
		[SerializeField]
		private string _key;
		public string Key
		{
			get { return _key; }
			set { _key = value; }
		}

		[SerializeField]
		private Sprite _sprite;
		public Sprite Sprite
		{
			get { return _sprite; }
			set { _sprite = value; }
		}
	}
}
