using UnityEngine;

public class DashCard : ActiveCard
{
	[System.Serializable]
	struct DashProperties
	{
		[Min(0)] public float dashSpeed; // = 8f;
		[Min(0)] public float dashTime; // = 0.15f;
		[TextArea] public string upgradeBlurb;
	}

	[Header("Dash Card Properties")]
	[SerializeField] DashProperties[] dashUpgrades;

	float dashTimer = 0f;
	bool dashing = false;
	Rigidbody2D playerRB;

	Vector2 dashDirection;
	Camera mainCamera;

	public override void OnPickup(int _dupeTimes)
	{
		playerRB = playerObj.GetComponent<Rigidbody2D>();
		mainCamera = Camera.main;

		base.OnPickup(_dupeTimes);
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
			playerRB.MovePosition(playerRB.position + dashDirection * GetCurrentDash().dashSpeed * Time.fixedDeltaTime);

			if (dashTimer >= GetCurrentDash().dashTime)
				dashing = false;
		}
	}

	DashProperties GetCurrentDash()
	{
		return dashUpgrades[Mathf.Clamp(GetDupeTimes() - 1, 0, dashUpgrades.Length - 1)];
	}

	public override int GetMaxDupeTimes()
	{
		return dashUpgrades.Length;
	}

	public override string GetBlurb()
	{
		int i = GetDupeTimes();
		if (i >= 0 && i < dashUpgrades.Length)
			return dashUpgrades[i].upgradeBlurb;
		return "";
	}
}