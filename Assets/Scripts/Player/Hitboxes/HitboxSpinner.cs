using UnityEngine;

public class HitboxSpinner : MonoBehaviour
{
	Vector2 startPosition;

	[SerializeField] float spinSpeed = 360f;
	[SerializeField] bool affectedByAttackSpeedUpgrade = true;
	float angle;

	[SerializeField] float targetMagnitude = 0.5f;
	[SerializeField, Min(0)] float magSpeed = 1f;
	float magnitude;

	void Start()
	{
		startPosition = transform.localPosition;

		Hitbox hitbox = GetComponent<Hitbox>();
		Vector2 dir = hitbox == null ? startPosition : hitbox.GetDirection();
		angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

		magnitude = startPosition.magnitude;

		if (affectedByAttackSpeedUpgrade && PlayerStatus.Instance != null)
			spinSpeed *= PlayerStatus.Instance.playerAS;
	}

	void Update()
	{
		bool changedPos = false;
		if (spinSpeed != 0)
		{
			angle += spinSpeed * Time.deltaTime;
			changedPos = true;
		}

		if (magnitude != targetMagnitude)
		{
			float sign = Mathf.Sign(targetMagnitude - magnitude);
			magnitude += magSpeed * sign * Time.deltaTime;
			if (sign != Mathf.Sign(targetMagnitude - magnitude))
				magnitude = targetMagnitude;
			changedPos = true;
		}

		if (changedPos)
		{
			float radians = angle * Mathf.Deg2Rad;
			transform.localPosition = (new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * magnitude);
		}
	}
}