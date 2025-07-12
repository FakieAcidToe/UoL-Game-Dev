using UnityEngine;

public class CardsManagerManager : MonoBehaviour
{
	[SerializeField] CardsManager activeCardsManager;
	[SerializeField] CardsManager passiveCardsManager;

	public CardsManager GetActiveCardsManager() { return activeCardsManager; }
	public CardsManager GetPassiveCardsManager() { return passiveCardsManager; }

	public void GenerateCardInHand(AbstractCard _card)
	{
		CardsManager manager = _card.GetComponent<ActiveCard>() ? activeCardsManager : passiveCardsManager;
		manager.GenerateCardInHand(_card);
	}
}