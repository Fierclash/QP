// 

using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using VisualNovelEngine.Environment;

namespace MyVisualNovel.Debugging
{
	public class BackgroundDebugger : MonoBehaviour
	{
		public string key;
		public SpriteRenderer backgroundRenderer;


		[TitleGroup("Debug")]
		[Button("Quinoa")]
		public void SetBackgroundQuinoa()
		{
			SetBackground("Quinoa's Bedroom");
		}

		[TitleGroup("Debug")]
		[Button("MF")]
		public void SetBackgroundMF()
		{
			SetBackground("John's Kitchen");
		}

		[TitleGroup("Debug")]
		[Button("Enoch")]
		public void SetBackgroundEnoch()
		{
			SetBackground("Enoch's Domain");
		}

		[TitleGroup("Debug")]
		[Button("Bex")]
		public void SetBackgroundBex()
		{
			SetBackground("Bex's Nightmare");
		}


		public void SetBackground(string key)
		{
			object[] sos = Resources.LoadAll("Background Data");
			List<BackgroundSO> backgroundSOs = sos.Where(x => x is BackgroundSO)
														.Select(x => x as BackgroundSO)
														.ToList();

			BackgroundSO backgroundSO = backgroundSOs.Where(x => (x as BackgroundSO).Value.Key.Equals(key))
													.FirstOrDefault() as BackgroundSO;
			backgroundRenderer.sprite = backgroundSO.Value.Sprite;
		}
	}
}
