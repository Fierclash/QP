using UnityEngine;

namespace Fierclash.DataStruct
{
	/// <summary>
	/// ScriptableObject containing bool data.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[CreateAssetMenu(fileName = "BoolSO_", menuName = "Fierclash/Data Struct/bool")]
	public sealed class BoolSO : BaseSO<bool> { }
}
