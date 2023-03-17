using UnityEngine;
using Fierclash.DataStruct;

namespace Fierclash
{
	/// <summary>
	/// ScriptableObject containing Audio Clip data.
	/// </summary>
	[CreateAssetMenu(fileName = "AudioClipSO_", menuName = "Fierclash/Data Struct/AudioClip")]
	public class AudioSO : BaseSO<AudioClip>
	{
		[SerializeField]
		[Range(0f, 1f)]
		private float _volume = 1f;

		public float Volume
		{
			get { return _volume; }
			set { _volume = value; }
		}
	}
}