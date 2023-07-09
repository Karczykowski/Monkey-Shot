using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum MovementEnum
{
    Backtracking,
    Linear,
    Circle
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int money = 0;
    public TextMeshProUGUI moneyText;
    private ComboSystem _comboSystem;
    public int currentGunIndex;
    public int newGunIndex;
    public List<Gun> guns;
    public List<GameObject> gunsObjects;
    public GameObject shop;
    public GameObject shopIcon;
    public GameObject zoomIcon;
    private bool shopToggle = false;
    public bool isZoomed = false;

    public bool isShotgunBought = false;
    public bool isSniperBought = false;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        _comboSystem = GetComponent<ComboSystem>();

        SetGunActive(currentGunIndex);

        newGunIndex = currentGunIndex;
    }
    void Update()
    {
        if (currentGunIndex == 2)
            zoomIcon.SetActive(true);
        else
            zoomIcon.SetActive(false);

        if(currentGunIndex!=newGunIndex)
        {
            ChangeWeapon();
        }

        if(_comboSystem.scoreMultiplier == 1)
        {
            moneyText.SetText("POINTS: " + money.ToString());
        }
        else
        {
            string multiplierPart = "<color=#ECAD47><size=40>" + " (x2)" + "</size></color>";
            moneyText.SetText("POINTS: " + money.ToString() + multiplierPart);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(shopToggle)
            {
                CloseShop();
            }
            else
            {
                OpenShop();
            }
        }
    }

    public void IncreasePoints(int number)
    {
        money += number;
    }

    public void EquipUpgrade(UpgradeTemplate newUpgrade)
    {
        for(int i = 0; i < guns.Count; i++)
        {
            guns[i].EquipUpgrade(newUpgrade);
        }
    }

    public void SetGunActive(int indexToActivate)
    {
        gunsObjects[indexToActivate].SetActive(true); 
    }

    public void ChangeWeapon()
    {
        gunsObjects[currentGunIndex].SetActive(false);
        gunsObjects[newGunIndex].SetActive(true);
        currentGunIndex = newGunIndex;
    }

    public void BuyAmmo()
    {
        if (money >= 10)
        {
            gunsObjects[currentGunIndex].GetComponent<Gun>().totalAmmo += 20;
            money -= 10;
        }
    }

    public void CloseShop()
    {
        shopIcon.SetActive(true);
        shop.SetActive(false);
        shopToggle = false;
        gunsObjects[currentGunIndex].GetComponent<Gun>().isShopOpen = false;
    }

    public void OpenShop()
    {
        shopIcon.SetActive(false);
        shop.SetActive(true);
        shopToggle = true;
        gunsObjects[currentGunIndex].GetComponent<Gun>().isShopOpen = true;
        for (int i = 0; i < shop.transform.childCount; i++)
        {
            Transform child = shop.transform.GetChild(i);
            if (i == currentGunIndex)
                child.gameObject.SetActive(true);
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}


