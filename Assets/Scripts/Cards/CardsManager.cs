using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
	[SerializeField] AbstractCard cardPrefab;

	[SerializeField] Transform cardCanvasTransform;

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
		// switch cards with scroll wheel
		if (cardsInHand.Count > 0)
		{
			float scroll = Input.GetAxis("Mouse ScrollWheel");

			if (scroll > 0f) // scroll up
			{
				int oldCard = selectedCard;
				selectedCard = (cardsInHand.Count + selectedCard - 1) % cardsInHand.Count;

				OrganizeCard(oldCard);
				OrganizeCard(selectedCard);
			}
			else if (scroll < 0f) // scroll down
			{
				int oldCard = selectedCard;
				selectedCard = (selectedCard + 1) % cardsInHand.Count;

				OrganizeCard(oldCard);
				OrganizeCard(selectedCard);
			}
		}

		// use selected card with space
		if (Input.GetKeyUp(KeyCode.Space))
		{
			if (selectedCard >= 0 && selectedCard < cardsInHand.Count)
			{
				AbstractCard card = cardsInHand[selectedCard];
				if (card != null)
				{
					card.UseCardEffect();
					cardsInHand.RemoveAt(selectedCard);
					card.DestroySelf();

					if (cardsInHand.Count > 0)
					{
						selectedCard %= cardsInHand.Count;
						OrganizeHand();
					}
					else
						selectedCard = 0;
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
					(Mathf.Cos(Mathf.Deg2Rad * angle) - 1) * tiltOffsetDist + (_cardNo == selectedCard ? selectedCardY : 0));
				card.SetTargetRotation(_cardNo == selectedCard ? 0 : angle);
			}
		}
	}
}
