using UnityEngine;

namespace Fierclash.DataStruct
{
	/// <summary>
	/// ScriptableObject containing Sprite data.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[CreateAssetMenu(fileName = "SpriteSO_", menuName = "Fierclash/Data Struct/Sprite")]
	public sealed class SpriteSO : BaseSO<Sprite> { }
}
