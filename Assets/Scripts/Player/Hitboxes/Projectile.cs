using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : Hitbox
{
	[Header("Projectile Properties")]
	[SerializeField, Tooltip("Projectile movement speed")] float speed = 1f;
	[SerializeField, Tooltip("Speed increase")] float acceleration = 0f;
	[SerializeField] float minSpeed = 0f;
	[SerializeField] float maxSpeed = 1f;

	[SerializeField, Min(0)] int richochetTimes = 0;
	[SerializeField] DirectionProperties.DirectionProperty richochetDirection = DirectionProperties.DirectionProperty.NearestEnemy;
	int richochetCounter = 0;

	Rigidbody2D rb;

	protected override void Awake()
	{
		base.Awake();

		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		if (speed != 0)
			rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
		speed = Mathf.Clamp(speed + acceleration * Time.fixedDeltaTime, minSpeed, maxSpeed);
	}

	protected override void DamageEnemy(EnemyHP _enemy)
	{
		base.DamageEnemy(_enemy);
		if (gameObject == null) return; // if destroyed, end

		// richochet
		if (richochetCounter < richochetTimes)
		{
			++richochetCounter;
			SetDirection(
				DirectionProperties.GetDirectionFromProperty(richochetDirection, transform, gameObject, hitEnemies),
				true
			);
		}
	}

	void OnValidate()
	{
		minSpeed = Mathf.Min(minSpeed, speed);
		maxSpeed = Mathf.Max(maxSpeed, speed);
	}
}