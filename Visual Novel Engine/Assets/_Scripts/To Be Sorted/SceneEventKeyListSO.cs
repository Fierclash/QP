// 

using System.Collections.Generic;
using UnityEngine;
using Fierclash.DataStruct;

namespace VisualNovelEngine.Core
{
	[System.Serializable]
	public class SceneEventKeyList
	{
		[SerializeField]
		private string _route;
		public string Route
		{
			get { return _route; }
			set { _route = value; }
		}

		[SerializeField]
		private List<string> _keys = new List<string>();
		public List<string> Keys
		{
			get { return _keys; }
			set { _keys = value; }
		}
	}

	[CreateAssetMenu(fileName = "SceneEventKeyListSO_", menuName = "Visual Novel Engine/Data Struct/SceneEventKeyList")]
	public class SceneEventKeyListSO : BaseSO<SceneEventKeyList> { }
}
