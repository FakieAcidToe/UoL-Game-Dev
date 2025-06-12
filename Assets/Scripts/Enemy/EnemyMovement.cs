using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
	[SerializeField] float moveSpeed = 2f;

	Rigidbody2D rb;
	Vector2 movement;

	Transform target;

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
		if (target == null)
			movement = Vector2.zero;
		else
			movement = target.position - transform.position;

		// Normalize diagonal movement
		if (movement.sqrMagnitude > 1) movement.Normalize();
	}

	void FixedUpdate()
	{
		// move the player using physics
		rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
	}
}
