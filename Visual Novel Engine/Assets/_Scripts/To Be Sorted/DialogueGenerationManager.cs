// #LogicScript

// Uses reflection to instantiate an object given its class name

using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MyVisualNovel.Editor
{
	public class DialogueGenerationManager
	{
		public static DialogueBatchConverter GetConverter(string conversionMethod)
		{
			var assembly = Assembly.GetExecutingAssembly();
			Type baseType = typeof(DialogueBatchConverter);
			var type = assembly.GetTypes()
					.Where(baseType.IsAssignableFrom)
					.First(t => baseType != t && t.Name.Equals(conversionMethod));

			return (DialogueBatchConverter)Activator.CreateInstance(type);
		}
	}
}
