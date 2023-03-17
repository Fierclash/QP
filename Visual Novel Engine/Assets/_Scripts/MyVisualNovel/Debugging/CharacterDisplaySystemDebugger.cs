// #DebugScript

using UnityEngine;
using Sirenix.OdinInspector;
using Fierclash.DataStruct;

namespace VisualNovelEngine
{
	public class CharacterDisplaySystemDebugger : MonoBehaviour
	{
		[TitleGroup("Components")]
		public CharacterSpriteDisplaySystem system;

		public string characterName = "Mike";
		public string expression = "Neutral";

		[TitleGroup("Debug")]
		[ButtonGroup("Debug/Character")]
		[Button("Mike"), DisableInEditorMode]
		public void SetCharacterMike()
		{
			characterName = "Mike";
			SetSprite();
		}

		[ButtonGroup("Debug/Character")]
		[Button("Carlos"), DisableInEditorMode]
		public void SetCharacterCarlos()
		{
			characterName = "Carlos";
			SetSprite();
		}

		[ButtonGroup("Debug/Character")]
		[Button("Sam"), DisableInEditorMode]
		public void SetCharacterSam()
		{
			characterName = "Sam";
			SetSprite();
		}

		[ButtonGroup("Debug/Character")]
		[Button("Wiwum"), DisableInEditorMode]
		public void SetCharacterWiwum()
		{
			characterName = "Wiwum";
			SetSprite();
		}

		[ButtonGroup("Debug/Character")]
		[Button("MF"), DisableInEditorMode]
		public void SetCharacterMF()
		{
			characterName = "MF";
			SetSprite();
		}

		[ButtonGroup("Debug/Character")]
		[Button("Enoch"), DisableInEditorMode]
		public void SetCharacterEnoch()
		{
			characterName = "Enoch";
			SetSprite();
		}

		[ButtonGroup("Debug/Character")]
		[Button("HoeBean"), DisableInEditorMode]
		public void SetCharacterHoeBean()
		{
			characterName = "HoeBean";
			SetSprite();
		}

		[ButtonGroup("Debug/Character")]
		[Button("Zed"), DisableInEditorMode]
		public void SetCharacterZed()
		{
			characterName = "Zed";
			SetSprite();
		}

		[ButtonGroup("Debug/Character")]
		[Button("Bex"), DisableInEditorMode]
		public void SetCharacterBex()
		{
			characterName = "Bex";
			SetSprite();
		}

		[ButtonGroup("Debug/Expressions")]
		[Button("Neutral"), DisableInEditorMode]
		public void SetExpressionNeutral()
		{
			expression = "Neutral";
			SetSprite();
		}

		[ButtonGroup("Debug/Expressions")]
		[Button("Happy"), DisableInEditorMode]
		public void SetExpressionHappy()
		{
			expression = "Happy";
			SetSprite();
		}

		[ButtonGroup("Debug/Expressions")]
		[Button("Angry"), DisableInEditorMode]
		public void SetExpressionAngry()
		{
			expression = "Angry";
			SetSprite();
		}

		[ButtonGroup("Debug/Expressions")]
		[Button("Sad"), DisableInEditorMode]
		public void SetExpressionSad()
		{
			expression = "Sad";
			SetSprite();
		}

		public void SetSprite()
		{
			CharacterDisplayData data = new CharacterDisplayData()
			{
				Sprite = ExtractSprite(characterName, expression),
			};
			system.SetCharacterSprite(data);
		}

		private Sprite ExtractSprite(string characterName, string expression)
		{
			string path = $"Character Sprites/SpriteSO_{characterName}_{expression}";
			SpriteSO spriteSO = Resources.Load(path) as SpriteSO;
			if (spriteSO == null) return null;
			Sprite sprite = spriteSO.Value;
			Resources.UnloadAsset(spriteSO);

			return sprite;
		}
	}
}
