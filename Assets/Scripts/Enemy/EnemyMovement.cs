using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
	[SerializeField] float moveSpeed = 2f;

	Rigidbody2D rb;
	Vector2 movement;

	Transform target;

	[SerializeField] float hitstunTime = 0.05f;
	float hitstun = 0; // time left in hitstun (cant move)

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	public void SetTarget(Transform _target)
	{
		target = _target;
	}

	void Update()
	{

		if (hitstun > 0)
		{
			movement = Vector2.zero;
			hitstun -= Time.deltaTime;
			if (hitstun < 0) hitstun = 0;
		}
		else
		{
			if (target == null) movement = Vector2.zero;
			else movement = target.position - transform.position;

			// Normalize diagonal movement
			if (movement.sqrMagnitude > 1) movement.Normalize();
		}
	}

	void FixedUpdate()
	{
		// move the player using physics
		if (hitstun <= 0)
			rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
	}

	public void ReceiveKnockback(Vector2 _force)
	{
		rb.AddForce(_force, ForceMode2D.Impulse);
		hitstun = hitstunTime;
	}
}