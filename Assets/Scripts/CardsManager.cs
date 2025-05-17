using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
	[SerializeField] AbstractCard cardPrefab;

	[SerializeField] Transform cardCanvasTransform;

	[SerializeField] float xDistApart = 100f; // distance between cards in hand
	[SerializeField] float cardTilt = 10f; // angle to tilt cards in hand
	[SerializeField] float tiltOffsetDist = 50f; // distance to offset the cards depending on angle

	List<AbstractCard> cardsInHand;


	void Awake()
	{
		cardsInHand = new List<AbstractCard>();
	}

	void Update()
	{
		
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
		{
			AbstractCard card = cardsInHand[i];

			float angle = cardTilt * (cardsInHand.Count / 2 - i);
			card.SetTargetPosition(-xDistApart * (cardsInHand.Count - i - 1) + Mathf.Sin(Mathf.Deg2Rad * angle) * tiltOffsetDist, Mathf.Cos(Mathf.Deg2Rad * angle) * tiltOffsetDist);
			card.SetTargetRotation(angle);
		}
	}
}
