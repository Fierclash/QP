// #

using UnityEngine;
using Sirenix.OdinInspector;
using VisualNovelEngine.Dialogue;
using MyVisualNovel.Dialogue;
using System.Linq;
using System.Collections.Generic;
using VisualNovelEngine;
using VisualNovelEngine.Core;
using VisualNovelEngine.SceneEvents;
using Fierclash.DataStruct;

namespace MyVisualNovel.Debugging
{
	public class VisualNovelDebugger : MonoBehaviour
	{
		[TitleGroup("Components")]
		public DialogueWriterDebugger writer;
		public CharacterDisplaySystemDebugger displayer;
		public ChoicePanelDebugger choicer;

		public SettingTransitioner transitioner;
		public SceneEventKeyListSO subKeysSO;
		public SceneEventKeyListSO returnSO;
		public StringSO choiceSO;
		public StringSO settingSO;
		public BoolSO writingSO;
		public TweenDialogueTextWriter writerLogic;

		public GameObject choicesPanel;

		private KeyState state;
		private StateGraph graph;
		private string routeKey;
		public void Start()
		{
			routeKey = "Introduction";
			choicer.OnChoiceTriggered += TestWrite;
			writerLogic.OnWritingEnd += ShowChoicePanel;
			state = null;

			TestWrite();
		}

		[TitleGroup("Debug")]
		[ButtonGroup("Debug/Writer")]
		[Button("Write")]
		public void TestWrite()
		{
			//VisualNovelEngine.Dialogue.DialogueData data = GetRandomDialogue();
			Navigate();
			VisualNovelEngine.Dialogue.DialogueData data = GetGraphDialogue();
			if (data == null) return;

			writer.speaker = data.Speaker;
			writer.dialogue = data.Dialogue;
			writer.TestWrite();

			displayer.characterName = data.Speaker;
			displayer.expression = data.Expression;
			displayer.SetSprite();
		}

		private string scene = "Intro";

		private bool choiceReady = false;
		public void ShowChoicePanel()
		{
			// Use this if we need to locate SO from Resources during runtime
			// Get current selection from Database
			//object[] selectedChoiceArray = Resources.LoadAll("Game Data");
			//StringSO selectedChoiceSO = selectedChoiceArray.Where(x => x is StringSO)
			//											.Select(x => x as StringSO)
			//											.FirstOrDefault();
			object[] choiceSOArray1 = Resources.LoadAll("Choice Data");
			IEnumerable<ChoiceDataSO> choiceSOs1 = choiceSOArray1.Where(x => x is ChoiceDataSO)
														.Select(x => x as ChoiceDataSO)
														.Where(x => x.Value.Key.Equals(state.Key) && x.Value.Route.Equals(routeKey))
														.ToList();
			if (choiceReady)
			{
				Debug.Log(choiceSO.Value);
				choiceReady = false;
				choicesPanel.SetActive(true);
				List<string> choices = choiceSOs1.FirstOrDefault().Value.Choices;
				choicer.text = choices;
				choicer.SetButtonText();
				choicer.SetButtonCount(choices.Count);
			}
		}

		private void Navigate()
		{
			if (state == null)
			{
				// damn this is ugly ass code
				choiceSO.Value = "";
				choicesPanel.SetActive(false);
				SceneEventStateGraphGeneratorLogic logic = new SceneEventStateGraphGeneratorLogic();
				logic.entrySubkey = "ENTRY";
				logic.exitSubkey = "EXIT";
				logic.baseRouteSubkey = "O";

				object[] choiceSOArray = Resources.LoadAll("Choice Data");
				IEnumerable<ChoiceDataSO> choiceSOs = choiceSOArray.Where(x => x is ChoiceDataSO)
															.Select(x => x as ChoiceDataSO)
															.Where(x => x.Value.Route.Equals(routeKey))
															.ToList();

				foreach (var choiceData in choiceSOs)
				{
					logic.routeSubKeys.AddRange(choiceData.Value.Choices);
				}
				
				graph = logic.Generate(subKeysSO.Value.Keys);
				foreach (var fa in graph.Keys)
				{
					var k = (fa as KeyState).Key;
					var l = graph[fa].Select(x => (x as KeyState).Key);
					string s = string.Join(", ", l);
					Debug.Log($"{k}:: {s}");
				}

				state = graph.Keys.Where(x => (x as KeyState).SubKey.Equals(logic.entrySubkey))
									.FirstOrDefault() as KeyState;

				state = graph[state].FirstOrDefault() as KeyState;
			}
			else if (writingSO.Value)
			{
				return;
			}
			else
			{
				// Check if state is a branching state
				object[] choiceSOArray = Resources.LoadAll("Choice Data");
				IEnumerable<ChoiceDataSO> choiceSOs = choiceSOArray.Where(x => x is ChoiceDataSO)
															.Select(x => x as ChoiceDataSO)
															.Where(x => x.Value.Key.Equals(state.Key) && x.Value.Route.Equals(routeKey))
															.ToList();
				// Selecting a choice
				if (choiceSOs.Any())
				{
					// Use this if we need to locate SO from Resources during runtime
					// Get current selection from Database
					//object[] selectedChoiceArray = Resources.LoadAll("Game Data");
					//StringSO selectedChoiceSO = selectedChoiceArray.Where(x => x is StringSO)
					//											.Select(x => x as StringSO)
					//											.FirstOrDefault();
					Debug.Log(choiceSO.Value);

					KeyState choiceState = graph[state].Where(x => (x as KeyState).SubKey.Equals(choiceSO.Value)).FirstOrDefault() as KeyState;
					if (choiceState == null) return;
					state = choiceState;
					choicesPanel.SetActive(false);
				}
				else
				{
					if (!graph.ContainsKey(state) || graph[state].FirstOrDefault() == null)
					{
						Debug.LogError("State does not occur in graph, restarting state.");
						state = null;
						return;
					}

					state = graph[state].FirstOrDefault() as KeyState;
					if (state.SubKey.Equals("EXIT"))
					{
						Debug.LogWarning("End of Dialogue.");
						Debug.Log($"Navigated to {state.Key}");
						// Trigger Change in Script
						// TODO: do this next

						object[] routeLinkSOs = Resources.LoadAll("Route Key Link Data");
						List<RouteKeyLinkSO> routeLinks = routeLinkSOs.Where(x => x is RouteKeyLinkSO)
																	.Select(x => x as RouteKeyLinkSO)
																	.Where(x => x.Value.Start == routeKey)
																	.ToList();
						Debug.Log($"Found {routeLinks.Count} SOs");
						RouteKeyLinkSO routeLink = null;
						if (routeLinks.Count > 1)
						{
							routeLink = routeLinks.Where(x => x.Value.Choice.Equals(choiceSO.Value))
													.FirstOrDefault();
						}
						else
						{
							routeLink = routeLinks.FirstOrDefault();
						}

						
						object[] settingSOArray = Resources.LoadAll("Route Setting Link Data");
						RouteKeyLinkSO scar = settingSOArray.Where(x => x is RouteKeyLinkSO)
																	.Select(x => x as RouteKeyLinkSO)
																	.Where(x => x.Value.Start.Equals(routeLink.Value.End))
																	.FirstOrDefault();
						settingSO.Value = scar.Value.End;
						transitioner.TransitionQuick();


						// damn this is ugly ass code
						// Go to Return Scene
						choiceSO.Value = "";
						routeKey = routeLink.Value.End;
						choicesPanel.SetActive(false);
						object[] sceneKeys = Resources.LoadAll("Dialogue Data");
						SceneEventKeyListSO sceneKey = sceneKeys.Where(x => x is SceneEventKeyListSO)
																	.Select(x => x as SceneEventKeyListSO)
																	.Where(x => x.Value.Route.Equals(routeKey))
																	.FirstOrDefault();
						foreach (var key in sceneKeys)
						{
							if (key is not SceneEventKeyListSO) continue;
							var p = key as SceneEventKeyListSO;
							Debug.Log($"Route for {p.name} is {p.Value.Route} and is {p.Value.Route.Equals(routeKey)}");
						}

						Debug.Log($"Finding Keys for {routeKey} of list of {sceneKeys.Length}");
						if (sceneKey == null) Debug.LogError("no scene key found");
						subKeysSO = sceneKey;
						return;
					}
				}
			}


			// Check if state is a branching state
			object[] choiceSOArray1 = Resources.LoadAll("Choice Data");
			IEnumerable<ChoiceDataSO> choiceSOs1 = choiceSOArray1.Where(x => x is ChoiceDataSO)
														.Select(x => x as ChoiceDataSO)
														.Where(x => x.Value.Key.Equals(state.Key) && x.Value.Route.Equals(routeKey))
														.ToList();
			if (choiceSOs1.Any())
			{
				//choicesPanel.SetActive(true);
				//List<string> choices = choiceSOs1.FirstOrDefault().Value.Choices;
				//choicer.text = choices;
				//choicer.SetButtonText();
				//choicer.SetButtonCount(choices.Count);
				choiceReady = true;
			}

			Debug.Log($"Navigated to {state.Key}");

		}

		private VisualNovelEngine.Dialogue.DialogueData GetGraphDialogue()
		{
			if (state == null || state.Key == "0_EXIT")
			{
				state = null;
				return null;
			}

			string path = $"Dialogue Data";
			Debug.Log($"Retrieving from {path}");
			object[] dialogueSOArray = Resources.LoadAll(path);
			//Debug.Log(dialogueSOArray.Length);
			Debug.Log($"My state key is {state.Key}");
			List<DialogueSO> dialogueSOs = dialogueSOArray.Where(x => x is DialogueSO)
														.Select(x => x as DialogueSO)
														.Where(x => x.Value.Route.Equals(routeKey))
														.Where(x => x.Value.Key.Equals(state.Key))
														.ToList();
			//Debug.Log($"Found {dialogueSOs.Count} matches.");
			VisualNovelEngine.Dialogue.DialogueData data = dialogueSOs.FirstOrDefault() == null ? null : dialogueSOs.FirstOrDefault().Value;
			return data;
		}

		private VisualNovelEngine.Dialogue.DialogueData GetRandomDialogue()
		{
			object[] dialogueSOArray = Resources.LoadAll("Dialogue Data");
			List<DialogueSO> dialogueSOs = dialogueSOArray.Where(x => x is DialogueSO)
														.Select(x => x as DialogueSO)
														.ToList();
			int i = Random.Range(0, dialogueSOs.Count);
			VisualNovelEngine.Dialogue.DialogueData data = dialogueSOs[i].Value;
			return data;
		}
	}
}
