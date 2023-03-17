// #StructureScript

using VisualNovelEngine.Dialogue;

namespace MyVisualNovel.Dialogue
{
	public class BasicDialogueWritingSystem : DialogueWritingSystem
	{
		// Data
		public ITextUI speakerTextUI;
		public ITextUI dialogueTextUI;

		// Logic
		public ITextUIDataWriter speakerTextUIWriter;
		public ITextUIDataWriter dialogueTextUIWriter;

		// Structure
		public override void WriteDialogue(TextUIData data)
		{
			speakerTextUIWriter.Write(speakerTextUI, data);
			dialogueTextUIWriter.Write(dialogueTextUI, data);
		}
	}
}
