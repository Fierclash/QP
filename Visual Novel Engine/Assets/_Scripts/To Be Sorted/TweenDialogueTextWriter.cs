// #LogicScript

using Fierclash.DataStruct;
using System;
using System.Collections;
using UnityEngine;
using VisualNovelEngine.Dialogue;

namespace MyVisualNovel.Dialogue
{

	public class TweenDialogueTextWriter : MonoBehaviour, ITextUIDataWriter
	{
		[Range(0f, 1f)]
		public float writeTimeDelta;

		[Range(0, 100)]
		public int pauseTicks;

		public BoolSO writingSO;

		public event Action OnWritingEnd;
		public event Action OnWritingTick;

		private Coroutine writingCoroutine;

		public void Write(ITextUI textUI, TextUIData data)
		{
			if (data is IDialogueAttribute)
			{
				IDialogueAttribute attribute = data as IDialogueAttribute;

				if (writingCoroutine != null)
				{
					StopCoroutine(writingCoroutine);
					writingCoroutine = null;
					writingSO.Value = false;
					textUI.Text = attribute.Dialogue;
					OnWritingEnd?.Invoke();
				}
				else
				{
					writingCoroutine = StartCoroutine(IWrite(textUI, attribute.Dialogue));
				}
			}
		}

		private IEnumerator IWrite(ITextUI textUI, string text)
		{
			writingSO.Value = true;
			WaitForSeconds wait = new WaitForSeconds(writeTimeDelta);
			string writtenText = "";
			char prev = ' ';
			foreach (char c in text)
			{
				writtenText += c;
				textUI.Text = writtenText;
				OnWritingTick?.Invoke();
				yield return wait;

				if ((prev == '.' || prev == '?' || prev == '!') && c == ' ')
				{
					for (int i = 0; i < pauseTicks; i++)
					{
						yield return wait;
					}
				}
				prev = c;
			}

			writingCoroutine = null;
			writingSO.Value = false;
			OnWritingEnd?.Invoke();
		}
	}
}
