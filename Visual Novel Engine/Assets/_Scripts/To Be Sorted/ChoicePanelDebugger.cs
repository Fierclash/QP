
using System;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VisualNovelEngine.UI;
using Fierclash.DataStruct;

namespace MyVisualNovel.Debugging
{
	public class ChoicePanelDebugger : MonoBehaviour
	{
		public ButtonUIGroup buttonGroup;
		public TextUIGroup textUIGroup;

		public event Action OnChoiceTriggered;

		public List<string> text = new List<string>() { "Maryann", "Quinoa", "Mike", "MF" };
		public StringSO so; // Turn into writer

		int count = 0;

		public void Start()
		{
			buttonGroup.Link();
			buttonGroup.OnButtonClick += StoreChoice;
		}

		[Button]
		public void SetButtonCount(int count)
		{
			for (int i = 0; i < 4; i++)
			{
				buttonGroup.elements[i].gameObject.SetActive(i < count);
			}
		}

		[Button]
		public void SetButtonText()
		{
			for (int i = 0; i < text.Count; i++)
			{
				textUIGroup.elements[i].text = text[i];
			}
		}

		public void StoreChoice(int index)
		{
			so.Value = text[index];
			OnChoiceTriggered?.Invoke();
			// Raise Event to trigger system
		}
	}
}
