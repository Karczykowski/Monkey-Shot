using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade/New Upgrade")]
public class UpgradeTemplate : ScriptableObject
{
    public new string name;
    public int cost;
    public int magSize;
    public float rateOfFire;
    public float reloadTime;
    public float explosionRadius;
    public float zoomTimer;
    public GunTemplate compatibleGun;
}
