using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerSprite : MonoBehaviour
{
	[SerializeField] Image playerImg;
	[SerializeField] float animSpeed = 0.25f;

	Sprite[] playerAnimation;

	void Start()
	{
		if (PlayerStatus.Instance != null && PlayerStatus.Instance.selectedCharacter != null)
			playerAnimation = new Sprite[] { PlayerStatus.Instance.selectedCharacter.animationSet.idleSide1, PlayerStatus.Instance.selectedCharacter.animationSet.idleSide2 };
	}

	void Update()
	{
		if (playerAnimation != null)
		{
			playerImg.sprite = playerAnimation[Mathf.FloorToInt(Time.timeSinceLevelLoad / animSpeed) % playerAnimation.Length];
		}
	}
}