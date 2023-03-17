using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyVisualNovel.Core
{
    public abstract class Loader : MonoBehaviour
	{
		public virtual void Load() { }
		public virtual void Unload() { }
	}
}
