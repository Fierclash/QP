// #FrameworkScript

using UnityEngine;

namespace VisualNovelEngine
{
	public interface ISpriteWriter
	{
		public void Write(ISpriteDisplay display, DisplayData data);
	}
}
