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
        hpText.text = PlayerStatus.Instance.playerHP.ToString();
        atkText.text = (Mathf.RoundToInt(PlayerStatus.Instance.playerDMG * 100) + "%");
        defText.text = (Mathf.RoundToInt(PlayerStatus.Instance.playerDef * 100) + "%");
        critText.text = (Mathf.RoundToInt(PlayerStatus.Instance.playerCrit) + "%");
        spdText.text = (Mathf.RoundToInt(PlayerStatus.Instance.playerSPD * 100) + "%");
        purText.text =(Mathf.RoundToInt(PlayerStatus.Instance.playerPUR * 100) + "%");
        atkspdText.text = (Mathf.RoundToInt(PlayerStatus.Instance.playerAS * 100) + "%");
        cooldownText.text = (Mathf.RoundToInt(100 - (PlayerStatus.Instance.playerCD * 100)) + "%");
        expText.text = (Mathf.RoundToInt(PlayerStatus.Instance.playerEXP * 100) + "%");
        pointText.text = (Mathf.RoundToInt(PlayerStatus.Instance.playerPoint * 100) + "%");
        currentPointText.text = PlayerStatus.Instance.playerUpgradePoints.ToString();
    }
}