using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shop : MonoBehaviour
{
    public List<UpgradeTemplate> availableUpgrades;
    public ShopButton buttonPrefab;

    private void Start()
    {
        foreach (UpgradeTemplate upgrade in availableUpgrades)
        {
            ShopButton tmpButton = Instantiate(buttonPrefab, transform.position, Quaternion.identity, transform);
            tmpButton.Setup(upgrade);
        }
    }
}
