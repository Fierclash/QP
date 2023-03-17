using UnityEngine;

namespace Fierclash.DataStruct
{
	/// <summary>
	/// ScriptableObject containing char data.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[CreateAssetMenu(fileName = "CharSO_", menuName = "Fierclash/Data Struct/char")]
	public sealed class CharSO : BaseSO<char> { }
}
