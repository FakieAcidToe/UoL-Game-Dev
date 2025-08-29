using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStat : MonoBehaviour
{
    public Text hpText;
    public Text atkText;
    public Text defText;
    public Text critText;
    public Text spdText;
    public Text purText;
    public Text atkspdText;
    public Text cooldownText;
    public Text expText;
    public Text pointText;
    public Text currentPointText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = PlayerStatus.Instance.playerHP.ToString();
        atkText.text = (Mathf.RoundToInt(PlayerStatus.Instance.playerDMGB * 100) + "%");
        defText.text = (Mathf.RoundToInt(PlayerStatus.Instance.playerDMGR * 100) + "%");
        critText.text = (Mathf.RoundToInt(PlayerStatus.Instance.playerCrit) + "%");
        spdText.text = (Mathf.RoundToInt(PlayerStatus.Instance.playerSPD * 100) + "%");
        purText.text =(Mathf.RoundToInt(PlayerStatus.Instance.playerPUR * 100) + "%");
        atkspdText.text = (Mathf.RoundToInt(PlayerStatus.Instance.playerATKSPD * 100) + "%");
        cooldownText.text = (Mathf.RoundToInt(100 - (PlayerStatus.Instance.playerCD * 100)) + "%");
        expText.text = (Mathf.RoundToInt(PlayerStatus.Instance.playerEXP * 100) + "%");
        pointText.text = (Mathf.RoundToInt(PlayerStatus.Instance.playerPoint * 100) + "%");
        currentPointText.text = PlayerStatus.Instance.playerUpgradePoints.ToString();
    }
}
