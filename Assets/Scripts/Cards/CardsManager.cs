using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour
{
	[Header("Active and Passive CardsUIPositioning")]
	[SerializeField] CardsUIPositioning activeCards;
	[SerializeField] CardsUIPositioning passiveCards;

	[Header("Card Prefab Reference")]
	[SerializeField] AbstractCard[] cardPrefabs;

	[Header("Level Up Screen")]
	[SerializeField] GameObject levelUpScreen;
	[SerializeField] GameObject[] cardPositions;
	bool isLevelUpScreenShowing = false;

	public CardsUIPositioning GetActiveCardsPositioner() { return activeCards; }
	public CardsUIPositioning GetPassiveCardsPositioner() { return passiveCards; }

	public void GenerateCardInHand(AbstractCard _card)
	{
		CardsUIPositioning positioner = _card.GetComponent<ActiveCard>() ? activeCards : passiveCards;
		positioner.GenerateCardInHand(_card);
	}

	public void ReceiveCardInHand(AbstractCard _card)
	{
		CardsUIPositioning positioner = _card.GetComponent<ActiveCard>() ? activeCards : passiveCards;
		positioner.ReceiveCardInHand(_card);
	}

	public void GenerateRandomCardInHand()
	{
		AbstractCard randomCard = PickRandomCard();
		if (randomCard != null) GenerateCardInHand(randomCard);
	}

	public void LevelUp()
	{
		if (isLevelUpScreenShowing) return;

		isLevelUpScreenShowing = true;
		Time.timeScale = 0f;
		levelUpScreen.SetActive(true);

		for (int i = 0; i < cardPositions.Length; ++i)
		{
			AbstractCard randomCard = PickRandomCard();
			if (randomCard != null)
			{
				AbstractCard newCard = Instantiate(randomCard, cardPositions[i].transform);
				Button cardButton = newCard.GetComponent<Button>();
				cardButton.enabled = true;
				int index = i;
				cardButton.onClick.AddListener(() => CardSelected(index));
			}
		}
	}

	void CardSelected(int index)
	{
		if (!isLevelUpScreenShowing) return;

		for (int i = 0; i < cardPositions.Length; ++i)
		{
			AbstractCard card = cardPositions[i].GetComponentInChildren<AbstractCard>();
			if (card != null)
			{
				Button cardButton = card.GetComponent<Button>();
				cardButton.onClick.RemoveAllListeners();
				cardButton.enabled = false;

				if (i == index) // this card is selected
					ReceiveCardInHand(card);
				else // card not selected
					Destroy(card.gameObject);
			}
		}

		isLevelUpScreenShowing = false;
		Time.timeScale = 1f;
		levelUpScreen.SetActive(false);
	}

	AbstractCard PickRandomCard()
	{
		if (cardPrefabs.Length > 0)
			return cardPrefabs[Random.Range(0, cardPrefabs.Length)];
		return null;
	}
}