using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public List<UpgradeTemplate> availableUpgrades;
    public ShopButton buttonPrefab;
    public static Shop instance;
    public Button shotgunButton;
    public Button sniperButton;
    public GameObject hotbarShotgun;
    public GameObject hotbarSniper;

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
        if (GameManager.instance.money >= 200)
        {
            GameManager.instance.isShotgunBought = true;
            shotgunButton.interactable = false;
            GameManager.instance.money -= 200;
            hotbarShotgun.SetActive(true);
        }
    }

    public void BuySniper()
    {
        if (GameManager.instance.money >= 200)
        {
            GameManager.instance.isSniperBought = true;
            sniperButton.interactable = false;
            GameManager.instance.money -= 200;
            hotbarSniper.SetActive(true);
        }
    }
}
