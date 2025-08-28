using UnityEngine;
using UnityEngine.UI;

public class DisplayStat : MonoBehaviour
{
	[SerializeField] Text hpText;
	[SerializeField] Text atkText;
	[SerializeField] Text defText;
	[SerializeField] Text critText;
	[SerializeField] Text spdText;
	[SerializeField] Text purText;
	[SerializeField] Text atkspdText;
	[SerializeField] Text cooldownText;
	[SerializeField] Text expText;
	[SerializeField] Text pointText;
	[SerializeField] Text currentPointText;

	void Update()
	{
		hpText.text = string.Format("{0,-12}: {1}", "HP", PlayerStatus.Instance.playerHP);
		atkText.text = string.Format("{0,-12}: {1}", "Attack", PlayerStatus.Instance.playerAtk);
		defText.text = string.Format("{0,-12}: {1}", "Defense", PlayerStatus.Instance.playerDef);
		critText.text = string.Format("{0,-12}: {1}", "CRIT Rate", PlayerStatus.Instance.playerCrit + "%");       
		spdText.text = string.Format("{0,-12}: {1}", "Movespeed", (PlayerStatus.Instance.playerSPD * 100)+ "%");
		purText.text = string.Format("{0,-12}: {1}", "Pick Up Range", (PlayerStatus.Instance.playerPUR * 100) + "%");
		atkspdText.text = string.Format("{0,-12}: {1}", "Attack Speed", (PlayerStatus.Instance.playerAS * 100) + "%");
		cooldownText.text = string.Format("{0,-12}: {1}", "Cooldown", ((100 - (PlayerStatus.Instance.playerCD * 100)) + "%"));
		expText.text = string.Format("{0,-12}: {1}", "EXP rate", (int)(PlayerStatus.Instance.playerEXP * 100) + "%");
		pointText.text = string.Format("{0,-12}: {1}", "Point rate", (int)(PlayerStatus.Instance.playerPoint * 100) + "%");
		currentPointText.text = string.Format("{0,-12}: {1}", "UP Points", PlayerStatus.Instance.playerUpgradePoints);
	}
}