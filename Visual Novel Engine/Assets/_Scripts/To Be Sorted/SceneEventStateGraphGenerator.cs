// 

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using VisualNovelEngine.Dialogue;
using VisualNovelEngine.Core;

// References: https://stackoverflow.com/questions/25461585/operator-overloading-equals

namespace VisualNovelEngine.SceneEvents
{
	public class KeyState : StateData, IKeyAttribute, IEquatable<KeyState>
	{
		public string Key
		{
			get { return string.Join('_', ID, SubKey); }
			set { }
		}

		public int ID
		{
			get;
			set;
		}

		public string SubKey
		{
			get;
			set;
		}


		// Implement IEquatable so KeyStates are equality comparable
		public static bool operator ==(KeyState obj1, KeyState obj2)
		{
			if (ReferenceEquals(obj1, obj2))
				return true;
			if (ReferenceEquals(obj1, null))
				return false;
			if (ReferenceEquals(obj2, null))
				return false;
			return obj1.Equals(obj2);
		}

		public static bool operator !=(KeyState obj1, KeyState obj2) => !(obj1 == obj2);

		public bool Equals(KeyState other)
		{
			if (ReferenceEquals(other, null)) return false;
			if (ReferenceEquals(this, other)) return true;
			return ID.Equals(other.ID)
					&& SubKey.Equals(other.SubKey);
		}

		public override bool Equals(object obj) => Equals(obj as KeyState);

		public override int GetHashCode()
		{
			unchecked
			{
				int hashCode = ID.GetHashCode();
				hashCode = (hashCode * 397) ^ SubKey.GetHashCode();
				return hashCode;
			}
		}
	}

	
	public class SceneEventStateGraphGenerator : MonoBehaviour
	{
		[Button]
		public void Test()
		{
			SceneEventStateGraphGeneratorLogic logic = new SceneEventStateGraphGeneratorLogic();

			logic.entrySubkey = "ENTRY";
			logic.exitSubkey = "EXIT";
			logic.baseRouteSubkey = "O";
			logic.routeSubKeys.AddRange(new List<string>{ "A", "B" });

			List<string> rawData = new List<string>
			{
				"1_O",
				"2_O",
				"3_O",
				"4_A",
				"4_B",
				"5_A",
				"5_B",
				"6_O",
				"7_A",
				"7_B",
				"8_A",
				"9_A",
				"10_O",
			};

			List<string> rawData1 = new List<string>
			{
				"7_B",
				"9_A",
				"1_O",
				"5_A",
				"8_A",
				"4_B",
				"4_A",
				"10_O",
				"7_A",
				"5_B",
				"6_O",
				"2_O",
				"3_O",
			};


			var graph = logic.Generate(rawData1);
			foreach (var key in graph.Keys)
			{
				var k = (key as KeyState).Key;
				var l = graph[key].Select(x => (x as KeyState).Key);
				string s = string.Join(", ", l);
				Debug.Log($"{k}:: {s}");
			}
		}
	}

	public class SceneEventStateGraphGeneratorLogic
	{
		public string entrySubkey;
		public string exitSubkey;
		public string baseRouteSubkey;

		public List<string> routeSubKeys = new List<string>();

		private class Key
		{
			public int id;
			public string subKey;

			// Implement IEquatable so KeyStates are equality comparable
			public static bool operator ==(Key obj1, Key obj2)
			{
				if (ReferenceEquals(obj1, obj2))
					return true;
				if (ReferenceEquals(obj1, null))
					return false;
				if (ReferenceEquals(obj2, null))
					return false;
				return obj1.Equals(obj2);
			}

			public static bool operator !=(Key obj1, Key obj2) => !(obj1 == obj2);

			public bool Equals(Key other)
			{
				if (ReferenceEquals(other, null)) return false;
				if (ReferenceEquals(this, other)) return true;
				return id.Equals(other.id)
						&& subKey.Equals(other.subKey);
			}

			public override bool Equals(object obj) => Equals(obj as Key);

			public override int GetHashCode()
			{
				unchecked
				{
					int hashCode = id.GetHashCode();
					hashCode = (hashCode * 397) ^ subKey.GetHashCode();
					return hashCode;
				}
			}
		}

		public StateGraph Generate(List<string> rawKeys)
		{
			// Parse strings into Keys
			// Key format is <id>_<subKey> (i.e., 1_a, 2_b)
			IEnumerable<Key> keys = rawKeys.Select(x =>
			{
				string[] parsedKey = x.Split('_');
				int id = int.Parse(parsedKey[0]);
				string subKey = parsedKey[1];
				return new Key()
				{
					id = id,
					subKey = subKey
				};
			});

			// Order by ID, then by Subkey
			List<Key> sortedKeys = keys.ToList()
										.OrderBy(x => x.id)
										.ThenBy(x => x.subKey)
										.Distinct()
										.ToList();
			List<int> sortedKeyIDs = sortedKeys.Select(x => x.id)
												.Distinct()
												.ToList();

			Dictionary<int, List<string>> idSubKeyMap = new Dictionary<int, List<string>>();
			foreach (Key key in sortedKeys)
			{
				if (idSubKeyMap.ContainsKey(key.id)) idSubKeyMap[key.id].Add(key.subKey);
				else idSubKeyMap.Add(key.id, new List<string>() { key.subKey });
			}

			// Populate StateGraph
			StateGraph graph = new StateGraph();
			foreach (Key key in sortedKeys)
			{
				//Debug.Log($"{key.id}, {key.subKey}");
				// Ensures each Key has an entry in the graph
				KeyState keyState = new KeyState()
				{
					ID = key.id,
					SubKey = key.subKey,
				};
				graph.Add(keyState, new List<StateData>());
			}


			// Connect keys on the graph if they are in same bucket and their IDs are adjacent
			int offSetCount = sortedKeyIDs.Last();
			List<Key> danglingKeys = new List<Key>();
			//Debug.Log($"Count is {offSetCount}");
			foreach (int i in sortedKeyIDs)
			{
				List<string> subKeys = idSubKeyMap[i];
				//Debug.Log($"Startin cycle {i} with {string.Join(',', subKeys)}");
				foreach (string subKey in subKeys)
				{
					// Base Route
					if (subKey.Equals(baseRouteSubkey))
					{
						bool foundNode = false;
						for (int j = i + 1; j <= offSetCount; j++)
						{
							var nextSubKeys = idSubKeyMap[j];
							foreach (string nextSubKey in nextSubKeys)
							{
								KeyState keyAState = new KeyState()
								{
									ID = i,
									SubKey = subKey,
								};
								KeyState keyBState = new KeyState()
								{
									ID = j,
									SubKey = nextSubKey,
								};

								graph[keyAState].Add(keyBState);
								foundNode = true;
							}

							if (foundNode) break;
						}

						if (!foundNode)
						{
							Debug.LogError($"Could not find node for {i}_{subKey}");
							danglingKeys.Add(new Key() { id = i, subKey = subKey });
						}
					}

					// Branching Route
					else if (routeSubKeys.Contains(subKey))
					{
						bool foundNode = false;
						for (int j = i + 1; j <= offSetCount; j++)
						{
							List<string> nextSubKeys = idSubKeyMap[j];
							Debug.Log($"Checking {j} with {string.Join(',', nextSubKeys)}");
							foreach (string nextSubKey in nextSubKeys)
							{
								if (nextSubKey.Equals(subKey) || nextSubKey.Equals(baseRouteSubkey))
								{
									Debug.Log($"Adding {j}_{nextSubKey} to {i}_{subKey}");
									KeyState keyAState = new KeyState()
									{
										ID = i,
										SubKey = subKey,
									};
									KeyState keyBState = new KeyState()
									{
										ID = j,
										SubKey = nextSubKey,
									};
									graph[keyAState].Add(keyBState);
									foundNode = true;
								}
							}

							if (foundNode) break;
						}

						if (!foundNode)
						{
							Debug.LogError($"Could not find node for {i}_{subKey}");
							danglingKeys.Add(new Key() { id = i, subKey = subKey });
						}
					}
					else
					{
						Debug.LogWarning($"SubKey {subKey} has no rule.");
					}
				}
			}


			// Append Entry and Exit states to the graph
			// [Entry] -> [Start] -> ... -> [End] -> [Exit]
			Key startKey = sortedKeys.First();
			KeyState startKeyState = new KeyState()
			{
				ID = startKey.id,
				SubKey = startKey.subKey,
			};
			KeyState entryKeyState = new KeyState()
			{
				ID = 0,
				SubKey = entrySubkey,
			};
			graph.Add(entryKeyState, new List<StateData>() { startKeyState });


			KeyState exitKeyState = new KeyState()
			{
				ID = 0,
				SubKey = exitSubkey,
			};

			foreach (Key key in danglingKeys)
			{
				KeyState endKeyState = new KeyState()
				{
					ID = key.id,
					SubKey = key.subKey,
				};
				graph[endKeyState].Add(exitKeyState);
			}

			return graph;
		}
	}
}
