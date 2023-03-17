using System.Collections.Generic;
using UnityEngine;

namespace Fierclash
{
    /// <summary>
    /// Scriptable object containing a set of audio clips
    /// </summary>
	[CreateAssetMenu(fileName = "AudioClipGroup_", menuName = "Fierclash/Audio/Audio Group")]
    public class AudioGroupSO : ScriptableObject
    {
        public List<AudioSO> clipList = new List<AudioSO>();

        public AudioSO GetRandom()
		{
            int index = Random.Range(0, clipList.Count);
            return clipList[index];
		}
    }
}