// #LogicScript

using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

namespace VisualNovelEngine.UI
{
	public class ButtonUIGroup : UIGroup<Button>
	{
		private List<Action> unsubscribers = new List<Action>();

		public event Action<int> OnButtonClick;

		public void Link()
		{
			Unlink();
			for (int i = 0; i < elements.Count; i++)
			{
				int iHost = i;
				Button button = elements[i];
				UnityAction raiseOnClick = () => RaiseOnControlPanelButtonClick(iHost);
				button.onClick.AddListener(raiseOnClick);
				unsubscribers.Add(() => button.onClick.RemoveListener(raiseOnClick));
			}
		}

		public void Unlink()
		{
			foreach (Action unsubscribe in unsubscribers) { unsubscribe?.Invoke(); }
		}

		private void RaiseOnControlPanelButtonClick(int i)
		{
			OnButtonClick?.Invoke(i);
		}
	}
}
