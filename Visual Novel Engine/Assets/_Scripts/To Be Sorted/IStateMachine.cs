// #FrameworkScript

using System.Collections.Generic;

namespace VisualNovelEngine.SceneEvents
{
	/// <summary>
	/// Framework for state-based scene events.
	/// </summary>
	public interface IStateMachine
	{
		public StateData State
		{
			get;
			set;
		}

		public StateGraph Graph
		{
			get;
			set;
		}
	}
}
