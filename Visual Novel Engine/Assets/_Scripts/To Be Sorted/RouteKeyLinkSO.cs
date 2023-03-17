// #DataStructScript

using UnityEngine;
using Fierclash.DataStruct;

namespace VisualNovelEngine.Core
{
	[System.Serializable]
	public class RouteKeyLink
	{
		[SerializeField]
		private string _start;
		public string Start
		{
			get { return _start; }
			set { _start = value; }
		}

		[SerializeField]
		private string _end;
		public string End
		{
			get { return _end; }
			set { _end = value; }
		}

		[SerializeField]
		private string _choice;
		public string Choice
		{
			get { return _choice; }
			set { _choice = value; }
		}
	}

	[CreateAssetMenu(fileName = "RouteKeyLinkSO_", menuName = "Visual Novel Engine/Data Struct/Route Key Link")]
	public class RouteKeyLinkSO : BaseSO<RouteKeyLink> { }
}
