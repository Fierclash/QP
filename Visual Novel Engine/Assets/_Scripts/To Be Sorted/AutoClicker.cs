// 

using UnityEngine;
using UnityEngine.UI;

namespace VisualNovelEngine
{
	public class AutoClicker : MonoBehaviour
	{
		public bool auto;
		public Button button;

		public void Update()
		{
			if(auto) button.onClick.Invoke();
		}
	}
}
