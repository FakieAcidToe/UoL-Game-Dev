using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
	void Start()
	{
		if (PlayerStatus.Instance != null)
			transform.localScale = Vector3.one * PlayerStatus.Instance.playerPUR;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.TryGetComponent(out ICollectible collectible))
			collectible.Collect();
	}
}