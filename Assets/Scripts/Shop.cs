using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public List<UpgradeTemplate> availableUpgrades;
    public ShopButton buttonPrefab;
    public Sprite shopMouseIcon;
    public static Shop instance;
    public Button ShotgunButton;
    public Button SniperButton;

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

    public void BuyShotgun()
    {
        GameManager.instance.isShotgunBought = true;
        ShotgunButton.interactable = false;
    }

    public void BuySniper()
    {
        GameManager.instance.isSniperBought = true;
        SniperButton.interactable = false;
    }
}
