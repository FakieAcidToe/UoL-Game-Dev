using UnityEngine;

public class CharselectManager : MonoBehaviour
{
	[SerializeField] int gameplaySceneIndex = 1;
	LevelManagement sceneChanger;

	[SerializeField] AbstractCard[] cards;
	[SerializeField] Vector2 startPosition = Vector2.down * 500;

	void Awake()
	{
		sceneChanger = GetComponent<LevelManagement>();
	}

	void Start()
	{
		foreach (AbstractCard card in cards)
		{
			card.SetTargetPosition(card.transform.localPosition.x, card.transform.localPosition.y);
			card.transform.localPosition = startPosition;
		}
	}

	public void SelectCharacter(PlayerEquipment selectedChar)
	{
		if (PlayerStatus.Instance != null)
		{
			PlayerStatus.Instance.selectedCharacter = selectedChar;
			sceneChanger.LoadLevelByIndex(gameplaySceneIndex);
		}
	}
}