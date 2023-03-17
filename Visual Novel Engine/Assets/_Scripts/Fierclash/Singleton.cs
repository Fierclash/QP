using UnityEngine;

namespace Fierclash
{
	/// <summary>
	/// Implements singleton pattern.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		/// <summary>
		/// Static reference to a singleton instance.
		/// </summary>
		public static T Instance
		{
			get;
			private set;
		}

		private void Awake()
		{
			// Set this object as the singleton instance
			if (Instance == null) Instance = this as T;
			// Otherwise, destroy this object
			else Destroy(this);
		}

		private void OnApplicationQuit()
		{
			//Instance = null;
			//Destroy(this);
		}
	}

	/// <summary>
	/// Implements singleton pattern with persisting behavior.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class PersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		/// <summary>
		/// Static reference to a singleton instance.
		/// </summary>
		public static T Instance
		{
			get;
			private set;
		}

		private void Awake()
		{
			// Set this object as the singleton instance
			if (Instance == null)
			{
				Instance = this as T;
				DontDestroyOnLoad(this);
			}
			// Destroy this object
			else
			{
				Destroy(this);
			}
		}

		private void OnApplicationQuit()
		{
			//Instance = null;
			//Destroy(this);
		}
	}


	/// <summary>
	/// Base Singleton with no implementation.
	/// </summary>
	public sealed class BaseSingleton : Singleton<BaseSingleton> { }
}
