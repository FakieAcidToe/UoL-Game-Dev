using UnityEngine;

public class DashCard : ActiveCard
{
	[Header("Dash Card Properties")]
	[SerializeField, Min(0)] float dashSpeed = 8f;
	[SerializeField, Min(0)] float dashTime = 0.15f;
	float dashTimer = 0f;
	bool dashing = false;
	Rigidbody2D playerRB;

	Vector2 dashDirection;
	Camera mainCamera;

	public override void OnPickup()
	{
		playerRB = playerObj.GetComponent<Rigidbody2D>();
		mainCamera = Camera.main;

		base.OnPickup();
	}

	public override void UseCardEffect()
	{
		if (CanBeUsed())
		{
			// use wasd movement to dash
			dashDirection.x = Input.GetAxisRaw("Horizontal");
			dashDirection.y = Input.GetAxisRaw("Vertical");

			if (dashDirection.magnitude <= 0) // use mouse to dash if wasd movement is 0
				dashDirection = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - playerObj.position);

			dashing = true;
			dashTimer = 0f;
			dashDirection.Normalize();
			TriggerCooldown();
		}
	}

	void FixedUpdate()
	{
		if (dashing)
		{
			dashTimer += Time.fixedDeltaTime;
			playerRB.MovePosition(playerRB.position + dashDirection * dashSpeed * Time.fixedDeltaTime);

			if (dashTimer >= dashTime)
				dashing = false;
		}
	}
}