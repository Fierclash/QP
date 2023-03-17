using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VisualNovelEngine.SceneEvents;

namespace MyVisualNovel.Core
{
	public class IntroSceneNavigatorLoader : MonoBehaviour
	{
		[Header("Navigator")]
		[SerializeField]
		private IntroSceneNavigator navigator;

		public void Start()
		{
			Load();
		}

		public void Load()
		{
		}
	}
}
