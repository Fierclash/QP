// 

using UnityEngine;
using Fierclash;
using Sirenix.OdinInspector;
using System.Collections;

namespace VisualNovelEngine
{
	public class TransitionCanvasPort : PersistentSingleton<TransitionCanvasPort>
	{
		public float fadeTime;
		public GameObject transitionCanvas;
		public Material material;

		private float Delta => 1f / fadeTime;

		private Coroutine fadeCoroutine;

		[Button]
		public void ShowCanvas()
		{
			if (fadeCoroutine != null)
			{
				StopCoroutine(fadeCoroutine); 
				fadeCoroutine = null;
			}
			fadeCoroutine = StartCoroutine(IShowCanvas());
		}

		private IEnumerator IShowCanvas()
		{
			material.SetFloat("_Offset", -1f);
			material.SetFloat("_RightOffset", -1f);
			transitionCanvas.gameObject.SetActive(true);
			for (float offset = -2f; offset < 2f; offset += Delta)
			{
				material.SetFloat("_Offset", offset);
				yield return null;
			}

			fadeCoroutine = null;
		}

		[Button]
		public void HideCanvas()
		{
			// Coroutine Override
			if (fadeCoroutine != null)
			{
				StopCoroutine(fadeCoroutine);
				fadeCoroutine = null;
			}
			StartCoroutine(IHideCanvas());
		}

		private IEnumerator IHideCanvas()
		{
			material.SetFloat("_Offset", -1f);
			material.SetFloat("_RightOffset", 1f);
			for (float offset = 2f; offset > -2f; offset -= Delta)
			{
				material.SetFloat("_RightOffset", offset);
				yield return null;
			}

			fadeCoroutine = null;
			transitionCanvas.gameObject.SetActive(false);
		}
	}
}
