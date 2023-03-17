// #StructuralScript

using System.Collections.Generic;
using UnityEngine;
using VisualNovelEngine.Core;
using VisualNovelEngine.SceneEvents;

namespace MyVisualNovel.Core
{
	public class IntroSceneNavigator : MonoBehaviour, IOpenNavigator, ILinearNavigator
	{
		public IStateEvaluator evaluator;

		public SceneEventStateMachine stateMachine;
		//private ConditionDictionary condition;
		//private ConditionResolver resolver;

		public void Navigate(NavigationData data)
		{
			if (data is IKeyAttribute)
			{
				IKeyAttribute attribute = data as IKeyAttribute;
				string key = attribute.Key;
			}
		}

		public void NavigateNext()
		{
			// Get State
			// GoTo "Next" state
			// Set state
			StateData state = stateMachine.State;
			ICollection<StateData> adjacentStates = stateMachine.GetAdjacentStates(state);
			EvaluationData parameters = new EvaluationData();
			StateData nextState = evaluator.Evaluate(adjacentStates, parameters);

			stateMachine.State = nextState;
		}

		public void NavigatePrevious()
		{
			// Get State
			// GoTo "Previous" state
			// Set state

			StateData state = stateMachine.State;
		}
	}
}
