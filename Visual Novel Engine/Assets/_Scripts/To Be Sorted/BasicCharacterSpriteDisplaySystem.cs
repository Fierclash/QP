// #StructureScript

using VisualNovelEngine;

namespace MyVisualNovel.Environment
{
	public sealed class BasicCharacterSpriteDisplaySystem : CharacterSpriteDisplaySystem
	{
		// Data
		public ISpriteDisplay characterSpriteDisplay;

		// Logic
		public ISpriteWriter spriteEditor;

		// Structure
		public override void SetCharacterSprite(DisplayData data)
		{
			spriteEditor.Write(characterSpriteDisplay, data);
		}
	}
}
