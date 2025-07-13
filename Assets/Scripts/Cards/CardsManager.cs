using UnityEngine;

public class CardsManager : MonoBehaviour
{
	[Header("Active and Passive CardsUIPositioning")]
	[SerializeField] CardsUIPositioning activeCards;
	[SerializeField] CardsUIPositioning passiveCards;

	[Header("Card Prefab Reference")]
	[SerializeField] AbstractCard[] cardPrefabs;

	public CardsUIPositioning GetActiveCardsPositioner() { return activeCards; }
	public CardsUIPositioning GetPassiveCardsPositioner() { return passiveCards; }

	public void GenerateCardInHand(AbstractCard _card)
	{
		CardsUIPositioning positioner = _card.GetComponent<ActiveCard>() ? activeCards : passiveCards;
		positioner.GenerateCardInHand(_card);
	}

	public void GenerateRandomCardInHand()
	{
		if (cardPrefabs.Length > 0)
			GenerateCardInHand(cardPrefabs[Random.Range(0, cardPrefabs.Length)]);
	}
}