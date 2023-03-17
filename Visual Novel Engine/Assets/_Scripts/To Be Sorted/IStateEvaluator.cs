// #FrameworkScript

using System.Collections.Generic;

namespace VisualNovelEngine.SceneEvents
{
	public class EvaluationData { }

	public interface IStateEvaluator
	{
		public StateData Evaluate(ICollection<StateData> state, EvaluationData parameters);
	}
}
