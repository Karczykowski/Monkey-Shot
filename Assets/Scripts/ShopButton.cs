using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    private UpgradeTemplate upgrade;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;

    public void Setup(UpgradeTemplate newUpgrade)
    {
        upgrade = newUpgrade;
        nameText.SetText(newUpgrade.name);
        costText.SetText(newUpgrade.cost.ToString());
    }

    public void Buy()
    {
        if (GameManager.instance.money < upgrade.cost)
            return;
        GameManager.instance.EquipUpgrade(upgrade);
        GameManager.instance.money -= upgrade.cost;
        GetComponent<Button>().interactable = false;
    }
}
