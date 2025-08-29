using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStatus : MonoBehaviour
{
    public GameObject hpBtn;
    public GameObject dmgbBtn;
    public GameObject dmgrBtn;
    public GameObject spdBtn;
    public GameObject critBtn;
    public GameObject purBtn;
    public GameObject atkspdBtn;
    public GameObject cdBtn;
    public GameObject expBtn;
    public GameObject pointBtn;

    public void ResetStats()
    {
        PlayerStatus.Instance.ResetUpgrades(hpBtn.GetComponent<UpgradeManagerV2>().baseUpgradeCost
            , dmgbBtn.GetComponent<UpgradeManagerV2>().baseUpgradeCost
            , dmgrBtn.GetComponent<UpgradeManagerV2>().baseUpgradeCost
            , spdBtn.GetComponent<UpgradeManagerV2>().baseUpgradeCost
            , critBtn.GetComponent<UpgradeManagerV2>().baseUpgradeCost
            , purBtn.GetComponent<UpgradeManagerV2>().baseUpgradeCost
            , atkspdBtn.GetComponent<UpgradeManagerV2>().baseUpgradeCost
            , cdBtn.GetComponent<UpgradeManagerV2>().baseUpgradeCost
            , expBtn.GetComponent<UpgradeManagerV2>().baseUpgradeCost
            , pointBtn.GetComponent<UpgradeManagerV2>().baseUpgradeCost
            );

        hpBtn.GetComponent<UpgradeManagerV2>().UpdateUpgradeStatus();
        dmgbBtn.GetComponent<UpgradeManagerV2>().UpdateUpgradeStatus();
        dmgrBtn.GetComponent<UpgradeManagerV2>().UpdateUpgradeStatus();
        spdBtn.GetComponent<UpgradeManagerV2>().UpdateUpgradeStatus();
        critBtn.GetComponent<UpgradeManagerV2>().UpdateUpgradeStatus();
        purBtn.GetComponent<UpgradeManagerV2>().UpdateUpgradeStatus();
        atkspdBtn.GetComponent<UpgradeManagerV2>().UpdateUpgradeStatus();
        cdBtn.GetComponent<UpgradeManagerV2>().UpdateUpgradeStatus();
        expBtn.GetComponent<UpgradeManagerV2>().UpdateUpgradeStatus();
        pointBtn.GetComponent<UpgradeManagerV2>().UpdateUpgradeStatus();
    }
}