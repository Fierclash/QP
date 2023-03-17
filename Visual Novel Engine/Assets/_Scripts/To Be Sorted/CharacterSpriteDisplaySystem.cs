
using System.Collections;
using UnityEngine;

namespace VisualNovelEngine
{
	public abstract class DisplayData { }

	public class CharacterDisplayData : DisplayData, ISpriteAttribute
	{
		public Sprite Sprite
		{
			get;
			set;
		}
	}

	public interface ISpriteDisplay
	{
		public Sprite Display
		{
			get;
			set;
		}
	}

	public interface ISpriteAttribute
	{
		public Sprite Sprite
		{
			get;
			set;
		}
	}

	public class StaticSpriteEditor : ISpriteWriter
	{
		public void Write(ISpriteDisplay display, DisplayData data)
		{
			if (data is ISpriteAttribute)
			{
				ISpriteAttribute attribute = data as ISpriteAttribute;
				display.Display = attribute.Sprite;
			}
		}
	}

	public abstract class CharacterSpriteDisplaySystem : MonoBehaviour
	{
		public virtual void SetCharacterSprite(DisplayData data) { }
	}
}
