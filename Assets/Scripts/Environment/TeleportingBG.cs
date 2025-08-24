using UnityEngine;

public class TeleportingBG : MonoBehaviour
{
	[Header("Reference")]
	[SerializeField] Transform target; // camera or player to track
	[SerializeField, Min(0), Tooltip("0 = Don't tile")] Vector2 repeatDistance = Vector2.right; // how far before repeating

	void Start()
	{
		if (target == null) target = Camera.main.transform;
	}

	void Update()
	{
		// if target moved forward past repeatDistance relative to this object
		if (repeatDistance.x > 0 && Mathf.Abs(target.position.x - transform.position.x) >= repeatDistance.x)
			// move object forward to repeat
			transform.position += Vector3.right * repeatDistance.x * Mathf.Sign(target.position.x - transform.position.x);

		if (repeatDistance.y > 0 && Mathf.Abs(target.position.y - transform.position.y) >= repeatDistance.y)
			transform.position += Vector3.up * repeatDistance.y * Mathf.Sign(target.position.y - transform.position.y);
	}
}