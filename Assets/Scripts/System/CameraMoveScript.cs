namespace NManager
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using DG.Tweening;
	using NMap;
	using NManager;

	public class CameraMoveScript : MonoBehaviour
	{
		[SerializeField]
		float offsetX;
		[SerializeField]
		float offsetY;
		public GameObject player;
		private Vector3 basePosition;
		private Vector3 shakeOffset = Vector3.zero;

		private void Start()
		{
			// 初期位置を設定
			basePosition = new Vector3(offsetX, player.transform.position.y + offsetY, -10);
			this.transform.position = basePosition;
		}

		public void ShakeCamera(float duration = 0.3f, float strength = 1.5f)
		{
			this.transform.DOComplete();
			DOTween.Shake(() => shakeOffset, x => shakeOffset = x, duration, strength).OnComplete(() => shakeOffset = Vector3.zero);
		}

		private void LateUpdate()
		{
			if (GameManager.instance.gameObject.GetComponent<MapManager>() != null)
			{
				float max = GameManager.instance.gameObject.GetComponent<MapManager>().mapEndAltitude - 5;
				Vector2 pos = player.transform.position;
				basePosition = new Vector3(offsetX, Mathf.Min(pos.y + offsetY, max), -100);
			}
			else
			{
				Vector2 pos = player.transform.position;
				basePosition = new Vector3(offsetX, pos.y + offsetY, -100);
			}


			// カメラの実際の位置をベース位置と揺れのオフセットの合計に設定
			this.transform.position = basePosition + shakeOffset;
		}
	}
}
