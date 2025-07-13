using UnityEngine;

[RequireComponent(typeof(PlayerAnimController))]
public class PlayerStatsSetter : MonoBehaviour
{
	[SerializeField] PlayerStats playerStats;
	PlayerAnimController animController;

	[SerializeField, Tooltip("Null = Automatically find game manager by tag")] CardsManager cardsManager;

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

		if (cardsManager != null)
			cardsManager.GenerateCardInHand(playerStats.weaponCard);
	}

	void OnValidate()
	{
		Awake();
	}
}