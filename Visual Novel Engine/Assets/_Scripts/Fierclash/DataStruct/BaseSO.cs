using UnityEngine;

namespace Fierclash.DataStruct
{
	/// <summary>
	/// Generic ScriptableObject for containing data of type T.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class BaseSO<T> : ScriptableObject
	{
		[Tooltip("Data SO holds.")]
		[SerializeField]
		private T _value;
		public T Value
		{
			get { return _value; }
			set { _value = value; }
		}
	}
}
