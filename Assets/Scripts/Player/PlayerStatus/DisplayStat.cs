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
