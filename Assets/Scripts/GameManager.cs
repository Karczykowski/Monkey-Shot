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
    private bool shopToggle = false;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        _comboSystem = GetComponent<ComboSystem>();

        for (int i = 0; i < gunsObjects.Count; i++)
        {
            gunsObjects[i].SetActive(false);
        }

        SetGunActive(currentGunIndex);

        newGunIndex = currentGunIndex;
    }
    void Update()
    {
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
                shop.SetActive(false);
                shopToggle = false;
                gunsObjects[currentGunIndex].GetComponent<Gun>().isShopOpen = false;
            }
            else
            {
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
}


