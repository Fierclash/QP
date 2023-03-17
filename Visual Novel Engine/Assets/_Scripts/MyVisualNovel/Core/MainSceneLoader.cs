// #LoaderScript

using Sirenix.OdinInspector;
using VisualNovelEngine.Dialogue;
using MyVisualNovel.Dialogue;
using MyVisualNovel.UI;
using MyVisualNovel.Environment;
using VisualNovelEngine;

namespace MyVisualNovel.Core
{
	public class MainSceneLoader : Loader
	{
		[TitleGroup("Dialogue")]
		public BasicDialogueWritingSystem writingSystem;
		public DialogueBarUIContainer container;
		public TweenDialogueTextWriter writer;

		[TitleGroup("Environment")]
		public BasicCharacterSpriteDisplaySystem displaySystem;
		public SpriteDisplay mainCharacterDisplay;

		public void OnEnable()
		{
			Load();
		}

		public void OnDisable()
		{
			Unload();
		}

		public override void Load()
		{
			LoadWritingSystem();
			LoadDisplaySystem();
		}

		public override void Unload()
		{

		}

		private void LoadWritingSystem()
		{
			writingSystem.speakerTextUI = container.speakerTextUI as ITextUI;
			writingSystem.dialogueTextUI = container.dialogueTextUI as ITextUI;

			writingSystem.speakerTextUIWriter = new StaticSpeakerTextWriter();
			writingSystem.dialogueTextUIWriter = writer;
		}

		private void LoadDisplaySystem()
		{
			displaySystem.characterSpriteDisplay = mainCharacterDisplay;
			displaySystem.spriteEditor = new StaticSpriteEditor();
		}
	}
}
