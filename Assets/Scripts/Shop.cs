using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shop : MonoBehaviour
{
    public List<UpgradeTemplate> availableUpgrades;
    public ShopButton buttonPrefab;
    public Sprite shopMouseIcon;
    public static Shop instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach (UpgradeTemplate upgrade in availableUpgrades)
        {
            ShopButton tmpButton = Instantiate(buttonPrefab, transform.position, Quaternion.identity, transform);
            tmpButton.Setup(upgrade);
        }
    }
}
