using UnityEngine;

[RequireComponent(typeof(PlayerAnimController))]
public class PlayerStatsSetter : MonoBehaviour
{
	[SerializeField] PlayerStats playerStats;
	PlayerAnimController animController;
	PlayerHP playerHP;

	[SerializeField, Tooltip("Null = Automatically find game manager by tag")] CardsManager cardsManager;
	[SerializeField, Tooltip("Null = Automatically find healthbar by tag")] HealthbarUI healthbar;

	void Awake()
	{
		if (playerStats != null)
		{
			// animation
			animController = GetComponent<PlayerAnimController>();
			if (playerStats.animationSet != null)
				animController.SetAnimations(playerStats.animationSet);

			// hp
			playerHP = GetComponent<PlayerHP>();
			if (healthbar != null)
			{
				playerHP.healthbar = healthbar;
				playerHP.SetMaxHP(playerStats.maxHP);
				playerHP.SetHP(playerStats.maxHP);
			}
		}
	}

	void Start()
	{
		if (cardsManager == null)
		{
			GameObject manager = GameObject.FindGameObjectWithTag("GameController");
			if (manager != null) cardsManager = manager.GetComponent<CardsManager>();
		}

		if (healthbar == null)
		{
			GameObject hpUI = GameObject.FindGameObjectWithTag("HealthbarUI");
			if (hpUI != null) healthbar = hpUI.GetComponent<HealthbarUI>();
		}

		if (cardsManager != null)
			cardsManager.GenerateCardInHand(playerStats.weaponCard);

		if (healthbar != null)
			healthbar.SetPortraitSprite(playerStats.animationSet.portrait);
	}

	void OnValidate()
	{
		Awake();
	}
}