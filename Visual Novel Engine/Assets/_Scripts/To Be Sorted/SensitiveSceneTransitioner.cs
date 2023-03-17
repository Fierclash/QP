// 

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VisualNovelEngine
{
	public class SensitiveSceneTransitioner : MonoBehaviour
	{
		public TransitionCanvasPort port;
		public Canvas canvas;
		public GameObject helperText;

		private bool locked = false;

		public void Start()
		{
			locked = true;
			port.material.SetFloat("_Offset", -1f);
			port.material.SetFloat("_RightOffset", -1f);
			helperText.SetActive(false);
			StartCoroutine(IPause());
		}

		private IEnumerator IPause()
		{
			yield return new WaitForSeconds(2f);
			helperText.SetActive(true);
			locked = false;
		}

		public void Update()
		{
			if (locked) return;

			if (Input.GetMouseButtonDown(0))
			{
				locked = true;
				StartCoroutine(ITransition());
			}
		}

		private IEnumerator ITransition()
		{
			locked = true;
			port.ShowCanvas();
			yield return new WaitForSeconds(1f);
			SceneManager.LoadScene(1);
			yield return new WaitForSeconds(1f);
			canvas.worldCamera = CameraSaver.Instance.main;
			port.HideCanvas();
		}
	}
}
