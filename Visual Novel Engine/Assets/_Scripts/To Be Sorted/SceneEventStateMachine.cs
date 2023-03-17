// #DataStructScript

using System.Collections.Generic;
using VisualNovelEngine.Core;

namespace VisualNovelEngine.SceneEvents
{
	public class SceneEventStateData : StateData, IKeyAttribute, INameAttribute
	{
		public string Key
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}
	}

	public class SceneEventStateMachine : IStateMachine
	{
		private StateData _data;
		private StateGraph _graph = new StateGraph();

		public StateData State
		{
			get { return _data; }
			set { _data = value; }
		}

		public StateGraph Graph
		{
			get { return _graph; }
			set { _graph = value; }
		}

		public List<StateData> GetAdjacentStates(StateData state)
		{
			List<StateData> states = new List<StateData>();

			if (Graph.ContainsKey(state))
			{
				List<StateData> adjacentStates = Graph[state];
				states.AddRange(adjacentStates);
			}

			return states;
		}
	}
}
