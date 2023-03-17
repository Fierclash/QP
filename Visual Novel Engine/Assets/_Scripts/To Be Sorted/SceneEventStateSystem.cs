// #StructureScript

using System.Collections.Generic;
using UnityEngine;
using VisualNovelEngine.SceneEvents;

namespace MyVisualNovel.Dialogue
{
	public interface ISceneEventDataWriter
	{
		public void Write(IStateMachine stateMachine, SceneEventData data);
	}

	public class SceneEventStateSystem : MonoBehaviour
	{
		// Data
		public IStateMachine sceneStateMachine;

		// Logic
		public ISceneEventDataWriter sceneStateWriter;
		public ISceneEventDataWriter sceneEventManager;

		// Structure
		public void NavigateNext()
		{

		}

		public void NavigatePrevious()
		{

		}

		public void Navigate(SceneEventData data)
		{
			sceneStateWriter.Write(sceneStateMachine, data);
			sceneEventManager.Write(sceneStateMachine, data);
		}
	}
}
