// #FrameworkScript

using System.Collections.Generic;
using UnityEngine;

namespace VisualNovelEngine.UI
{
	public abstract class UIGroup<T> : MonoBehaviour
	{
		[Header("Text UI")]
		public List<T> elements = new List<T>();
	}
}
