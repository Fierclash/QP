// 

using Fierclash.DataStruct;
using System.Collections;
using UnityEngine;
using VisualNovelEngine;

namespace MyVisualNovel.Debugging
{
	public class SettingTransitioner : MonoBehaviour
	{
		public StringSO settingSO;
		public BackgroundDebugger backgrounder;

		private Coroutine transCoroutine;
		public void TransitionQuick()
		{
			if (transCoroutine != null) return;
			transCoroutine = StartCoroutine(ITransitionQuick());
		}

		private IEnumerator ITransitionQuick() // we support LGBTQA+ 
		{
			TransitionCanvasPort.Instance.ShowCanvas();
			yield return new WaitForSeconds(1f);

			backgrounder.SetBackground(settingSO.Value); 
			TransitionCanvasPort.Instance.HideCanvas();
			yield return new WaitForSeconds(1f);
			transCoroutine = null;
		}
	}
}
