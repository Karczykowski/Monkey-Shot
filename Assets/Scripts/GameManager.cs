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
    public List<Gun> guns;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        _comboSystem = GetComponent<ComboSystem>();
    }
    void Update()
    {
        if(_comboSystem.scoreMultiplier == 1)
        {
            moneyText.SetText("POINTS: " + money.ToString());
        }
        else
        {
            string multiplierPart = "<color=#ECAD47><size=40>" + " (x2)" + "</size></color>";
            moneyText.SetText("POINTS: " + money.ToString() + multiplierPart);
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
}


