using UnityEngine;

public class CharselectManager : MonoBehaviour
{
	[SerializeField] int gameplaySceneIndex = 1;
	LevelManagement sceneChanger;

	void Awake()
	{
		sceneChanger = GetComponent<LevelManagement>();
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