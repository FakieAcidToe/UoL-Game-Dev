using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
	[SerializeField] AbstractCard cardPrefab;

	[SerializeField] Transform cardCanvasTransform;

	[Header("Card Positioning")]
	[SerializeField] bool selectable = false; // if cards can be selected
	[SerializeField] float xDistApart = 100f; // distance between cards in hand
	[SerializeField] float cardTilt = 10f; // angle to tilt cards in hand
	[SerializeField] float tiltOffsetDist = 200f; // distance to offset the cards depending on angle

	[SerializeField] float selectedCardY = 150; // added yPos of selected card

	List<AbstractCard> cardsInHand;

	int selectedCard;


	void Awake()
	{
		cardsInHand = new List<AbstractCard>();
		selectedCard = 0;
	}

	void Update()
	{
		if (!selectable) return;

		if (cardsInHand.Count > 0)
		{
			// switch cards with number key
			for (KeyCode key = KeyCode.Alpha0; key <= KeyCode.Alpha9; ++key)
			{
				if (Input.GetKeyDown(key))
				{
					int numberPressed = KeyCode.Alpha0 == key ? 9 : key - KeyCode.Alpha0 - 1;
					if (numberPressed < cardsInHand.Count)
						SelectCard(numberPressed);
				}
			}

			// switch cards with scroll wheel
			float scroll = Input.GetAxis("Mouse ScrollWheel");
			if (scroll > 0f) // scroll up
				SelectCard((cardsInHand.Count + selectedCard - 1) % cardsInHand.Count);
			else if (scroll < 0f) // scroll down
				SelectCard((selectedCard + 1) % cardsInHand.Count);
		}

		// use selected card with Space or E
		if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.E))
		{
			if (selectedCard >= 0 && selectedCard < cardsInHand.Count)
			{
				ActiveCard card = cardsInHand[selectedCard].GetComponent<ActiveCard>();
				if (card != null)
				{
					card.UseCardEffect();
					//RemoveCard(card);
				}
			}
		}
	}

	public void GenerateCardInHand()
	{
		AbstractCard newCard = Instantiate(cardPrefab, cardCanvasTransform);

		newCard.SetColour(Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f)); // random colour

		cardsInHand.Add(newCard);

		OrganizeHand();
	}

	public void RemoveCard(int _index)
	{
		if (selectedCard >= 0 && selectedCard < cardsInHand.Count)
			RemoveCard(cardsInHand[selectedCard]);
	}

	public void RemoveCard(AbstractCard _card)
	{
		cardsInHand.Remove(_card);
		_card.DestroySelf();

		if (cardsInHand.Count > 0)
		{
			selectedCard %= cardsInHand.Count;
			OrganizeHand();
		}
		else
			selectedCard = 0;
	}

	void OrganizeHand()
	{
		for (int i = 0; i < cardsInHand.Count; ++i)
			OrganizeCard(i);
	}

	void OrganizeCard(int _cardNo)
	{
		if (_cardNo >= 0 && _cardNo < cardsInHand.Count)
		{
			AbstractCard card = cardsInHand[_cardNo];
			if (card != null)
			{
				float angle = cardTilt * ((float)(cardsInHand.Count - 1) / 2 - _cardNo);
				card.SetTargetPosition(-xDistApart * (cardsInHand.Count - _cardNo - 1) + Mathf.Sin(Mathf.Deg2Rad * angle) * tiltOffsetDist,
					(Mathf.Cos(Mathf.Deg2Rad * angle) - 1) * tiltOffsetDist + ((selectable && _cardNo == selectedCard) ? selectedCardY : 0));
				card.SetTargetRotation((selectable && _cardNo == selectedCard) ? 0 : angle);
			}
		}
	}

	void SelectCard(int _cardNo)
	{
		if (cardsInHand.Count > 0 && cardsInHand.Count > _cardNo)
		{
			int oldCard = selectedCard;
			selectedCard = _cardNo;
			OrganizeCard(oldCard);
			OrganizeCard(selectedCard);
		}
	}
}