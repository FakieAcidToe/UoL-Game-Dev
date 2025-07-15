using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeManagerV2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string upgradeName;
    public int upgradeLVL;
    public int upgradeCost;

    public Text displayTextBox;
    private Coroutine resetCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUpgradeStatus();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void UpgradeStat()
    {
        PlayerStatus.Instance.UpdateStat(upgradeName, upgradeCost);
        UpdateUpgradeStatus();
        UpdateDisplay();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateDisplay();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void UpdateDisplay()
    {
        if (displayTextBox != null)
        {
            if (upgradeName == "ATK")
            {
                displayTextBox.text = "ATK UP \nQuantity " + (30 - upgradeLVL);
            }
            else if (upgradeName == "HP")
            {
                displayTextBox.text = "HP UP \nQuantity " + (30 - upgradeLVL);
            }
            else if (upgradeName == "CRIT")
            {
                displayTextBox.text = "CRIT UP \nQuantity " + (15 - upgradeLVL);
            }
            displayTextBox.text += "\nCost " + upgradeCost + "\nPoints: " + PlayerStatus.Instance.playerUpgradePoints;
        }
    }

    public void UpdateUpgradeStatus()
    {
        if (upgradeName == "ATK")
        {
            upgradeLVL = PlayerStatus.Instance.atkUpgrades;
        }
        else if (upgradeName == "HP")
        {
            upgradeLVL = PlayerStatus.Instance.hpUpgrades;

        }
        else if (upgradeName == "CRIT")
        {
            upgradeLVL = PlayerStatus.Instance.critUpgrades;

        }
        upgradeCost = (upgradeLVL + 1) * 100;
    }

}
