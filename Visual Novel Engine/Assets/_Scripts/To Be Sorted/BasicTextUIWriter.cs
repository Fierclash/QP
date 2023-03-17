// #LogicScript

using System.Collections;
using UnityEngine;
using VisualNovelEngine.Dialogue;

namespace MyVisualNovel.Dialogue
{
	public class StaticDialogueTextWriter : ITextUIDataWriter
	{
		public void Write(ITextUI textUI, TextUIData data)
		{
			if (data is IDialogueAttribute)
			{
				IDialogueAttribute attribute = data as IDialogueAttribute;
				textUI.Text = attribute.Dialogue;
			}
		}
	}

	public class StaticSpeakerTextWriter : ITextUIDataWriter
	{
		public void Write(ITextUI textUI, TextUIData data)
		{
			if (data is ISpeakerAttribute)
			{
				ISpeakerAttribute attribute = data as ISpeakerAttribute;
				textUI.Text = attribute.Speaker;
			}
		}
	}
}
