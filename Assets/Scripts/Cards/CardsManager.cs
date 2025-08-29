using System.Collections.Generic;
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
	[SerializeField, Min(0)] int maxNumOfWeapons = 3;
	[SerializeField, Min(0)] int maxNumOfActivatables = 3;
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
		AbstractCard randomCard = PickRandomCard(GetCurrentCardPool());
		if (randomCard != null) GenerateCardInHand(randomCard);
	}

	public void LevelUp()
	{
		if (isLevelUpScreenShowing) return;

		List<AbstractCard> cardPool = GetCurrentCardPool();
		if (cardPool.Count <= 0) return;

		isLevelUpScreenShowing = true;
		Time.timeScale = 0f;
		levelUpScreen.SetActive(true);

		List<uint> pickedCards = new List<uint>();

		for (int i = 0; i < cardPositions.Length; ++i)
		{
			Text textBlurb = cardPositions[i].GetComponentInChildren<Text>();
			textBlurb.text = "";

			AbstractCard randomCard = PickRandomCard(cardPool, pickedCards);
			if (randomCard != null)
			{
				AbstractCard newCard = Instantiate(randomCard, cardPositions[i].transform);
				Button cardButton = newCard.GetComponent<Button>();
				cardButton.enabled = true;
				int index = i;
				cardButton.onClick.AddListener(() => CardSelected(index));

				// set text blurb
				AbstractCard ownedCard = PlayerOwnsCard(randomCard.GetID());
				textBlurb.text = ownedCard == null ? randomCard.GetBlurb() : ownedCard.GetBlurb();

				pickedCards.Add(randomCard.GetID());
			}
		}
	}

	public void CardSelected(int index)
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

	AbstractCard PickRandomCard(List<AbstractCard> cardPool, List<uint> IDsToReject = null)
	{
		List < AbstractCard > cardPool2 = new List < AbstractCard >();
		foreach (AbstractCard card in cardPool)
			if (IDsToReject == null || !IDsToReject.Contains(card.GetID()))
				cardPool2.Add(card);

		if (cardPool2.Count > 0)
			return cardPool2[Random.Range(0, cardPool2.Count)];
		return null;
	}

	List<AbstractCard> GetCurrentCardPool()
	{
		List<AbstractCard> cardPool = new List<AbstractCard>();

		if (cardPrefabs.Length > 0)
		{
			int numOfWeaponsOwned = 0;
			int numOfActivatablesOwned = 0;
			foreach (AbstractCard card in passiveCards.cardsInHand) // passives only
				if (card is WeaponCard)
					++numOfWeaponsOwned;
			foreach (AbstractCard card in activeCards.cardsInHand) // actives only
				if (card is ActiveCard)
					++numOfActivatablesOwned;

			foreach (AbstractCard card in cardPrefabs)
			{
				if (card is WeaponCard)
				{
					AbstractCard ownedCard = PlayerOwnsPassiveCard(card.GetID());
					if ((numOfWeaponsOwned < maxNumOfWeapons && ownedCard == null) ||
						(ownedCard != null && ownedCard.GetDupeTimes() < ownedCard.GetMaxDupeTimes()))
						cardPool.Add(card);
				}
				else if (card is ActiveCard)
				{
					AbstractCard ownedCard = PlayerOwnsActiveCard(card.GetID());
					if ((numOfActivatablesOwned < maxNumOfActivatables && ownedCard == null) ||
						(ownedCard != null && ownedCard.GetDupeTimes() < ownedCard.GetMaxDupeTimes()))
						cardPool.Add(card);
				}
			}
		}
		return cardPool;
	}

	AbstractCard PlayerOwnsCard(uint id)
	{
		AbstractCard card = PlayerOwnsActiveCard(id);
		if (card != null) return card;
		card = PlayerOwnsPassiveCard(id);
		if (card != null) return card;
		return null;
	}

	AbstractCard PlayerOwnsActiveCard(uint id)
	{
		foreach (AbstractCard card in activeCards.cardsInHand)
			if (id == card.GetID())
				return card;
		return null;
	}

	AbstractCard PlayerOwnsPassiveCard(uint id)
	{
		foreach (AbstractCard card in passiveCards.cardsInHand)
			if (id == card.GetID())
				return card;
		return null;
	}
}