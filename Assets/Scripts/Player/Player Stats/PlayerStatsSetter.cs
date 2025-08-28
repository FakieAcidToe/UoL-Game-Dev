using UnityEngine;

[RequireComponent(typeof(PlayerAnimController))]
public class PlayerStatsSetter : MonoBehaviour
{
	[SerializeField] PlayerEquipment playerEquipment;
	PlayerAnimController animController;
	PlayerHP playerHP;

	[SerializeField, Tooltip("Null = Automatically find game manager by tag")] CardsManager cardsManager;
	[SerializeField, Tooltip("Null = Automatically find healthbar by tag")] HealthbarUI healthbar;

	void Awake()
	{
		if (PlayerStatus.Instance != null)
		{
			playerEquipment = PlayerStatus.Instance.selectedCharacter;

			if (PlayerStatus.Instance.selectedCharacter != null)
			{
				// hp
				playerHP = GetComponent<PlayerHP>();
				if (healthbar != null)
				{
					playerHP.healthbar = healthbar;
					playerHP.SetMaxHP(PlayerStatus.Instance.playerHP);
					playerHP.SetHP(PlayerStatus.Instance.playerHP);
				}
			}
		}

		if (playerEquipment != null)
		{
			// animation
			animController = GetComponent<PlayerAnimController>();
			if (playerEquipment.animationSet != null)
				animController.SetAnimations(playerEquipment.animationSet);
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
			cardsManager.GenerateCardInHand(playerEquipment.weaponCard);

		if (healthbar != null)
			healthbar.SetPortraitSprite(playerEquipment.animationSet.portrait);
	}

	void OnValidate()
	{
		Awake();
	}
}