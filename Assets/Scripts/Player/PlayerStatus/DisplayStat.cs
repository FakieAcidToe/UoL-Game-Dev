using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStat : MonoBehaviour
{
    public Text hpText;
    public Text atkText;
    public Text critText;
    public Text pointText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        hpText.text = string.Format("{0,-9}: {1}", "HP", PlayerStatus.Instance.playerHP);
        atkText.text = string.Format("{0,-9}: {1}", "Attack", PlayerStatus.Instance.playerAtk);
        critText.text = string.Format("{0,-9}: {1}", "CRIT Rate", PlayerStatus.Instance.playerCrit + "%");
        pointText.text = string.Format("{0,-9}: {1}", "UP Points", PlayerStatus.Instance.playerUpgradePoints);
    }
}
