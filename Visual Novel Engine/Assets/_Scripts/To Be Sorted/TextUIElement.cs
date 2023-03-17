
using UnityEngine;
using TMPro;
using VisualNovelEngine.Dialogue;

namespace MyVisualNovel.UI
{
    public class TextUIElement : TextMeshProUGUI, ITextUI
	{
		public string Text
		{
			get { return text; }
			set { text = value; }
		}
    }
}
