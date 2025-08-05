using UnityEngine;

[RequireComponent(typeof(PlayerAnimController))]
public class PlayerStatsSetter : MonoBehaviour
{
	[SerializeField] PlayerStats playerStats;
	PlayerAnimController animController;

	[SerializeField, Tooltip("Null = Automatically find game manager by tag")] CardsManager cardsManager;
	[SerializeField, Tooltip("Null = Automatically find healthbar by tag")] HealthbarUI healthbar;

	void Awake()
	{
		if (playerStats != null && playerStats.animationSet != null)
		{
			animController = GetComponent<PlayerAnimController>();
			animController.SetAnimations(playerStats.animationSet);
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