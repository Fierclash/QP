// #DataStructScript

using UnityEngine;

namespace VisualNovelEngine
{
	public class SpriteDisplay : MonoBehaviour, ISpriteDisplay
	{
		[Header("Components")]
		[SerializeField]
		private SpriteRenderer spriteRenderer;

		public Sprite Display
		{
			get { return spriteRenderer.sprite; }
			set { spriteRenderer.sprite = value; }
		}
	}
}
