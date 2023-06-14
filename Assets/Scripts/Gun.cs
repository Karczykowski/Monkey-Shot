using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Gun : MonoBehaviour
{
    public GunTemplate gunType;

    public static Vector2 mouseLocation;
    public float countdown = 0;         //shooting
    bool canShoot = true;
    bool isReloading = false;
    bool canMove = true;
    public int obstacleLayer;
    public int enemyLayer;

    public int totalAmmo=20;
    public int ammoInMag;
    public int magSize;
    public float rateOfFire;
    public float reloadTime;
    public TextMeshProUGUI reloadText;
    public TextMeshProUGUI ammoText;

    public GameObject shootEffect;
    public EnemySpawner enemySpawner;
    public SpriteRenderer crosshairRend;
    public SpriteRenderer handRend;
    public Transform cameraTransform;

    private List<UpgradeTemplate> equippedUpgrades; 
    //HandMovement
    [Range(0.2f, 1f)] public float movementSensitivity;

    private AnimationController _animController;
    private Vector2 startPos;

    private void Awake()
    {
        equippedUpgrades = new List<UpgradeTemplate>();
        _animController = GetComponent<AnimationController>();
    }
    private void Start()
    {
        PopulateInfo(gunType);
        startPos = transform.position;
    }
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 newPos = (Vector2)cameraTransform.position + startPos + (mousePos * movementSensitivity);

        if (canMove)
            transform.position = newPos;
        else
        {
            //movement when reloading
            transform.position = new Vector2(newPos.x, -17);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isReloading == false && ammoInMag!=magSize && totalAmmo!=0)
            {
                canShoot = false;
                StartCoroutine(Reload());
            }
        }

        ammoText.text = "AMMO: " + ammoInMag.ToString() + "/" + totalAmmo.ToString();
        if (ammoInMag > 0)
        {
            if (countdown <= 0 && isReloading==false)
            {
                canShoot = true;
                //reloadText.text = "Ready to shoot!";
            }
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
                //reloadText.text = "Next bullet: " + countdown.ToString("F1");
            }
        }
        else
        {
            if(totalAmmo==0)
            {
                reloadText.text = "OUT OF AMMO!";
            }
            else if (isReloading == false)
            {
                reloadText.text = "NEED TO RELOAD!";
            }
        }
    }
    void Shoot()
    {
        if (canShoot)
        {
            _animController.ChangeAnimationState("shot");
            AudioManager.instance.Play(gunType.shotSound);
            Shake.start = true;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hit = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
            RaycastHit2D target = new();

            for(int i = 0; i < hit.Length; i++)
            {
                if (hit[i].transform.gameObject.layer == obstacleLayer)
                {
                    Instantiate(shootEffect, ray.origin, Quaternion.identity);
                    ammoInMag--;
                    canShoot = false;
                    countdown = rateOfFire;
                    return;
                }
                if (hit[i].transform.gameObject.layer == enemyLayer)
                    target = hit[i];
            }
            if (target.collider != null)
            {
                Monkey targetEnemy = target.transform.GetComponent<Monkey>();
                targetEnemy.KillMonkey();
            }
            Instantiate(shootEffect, ray.origin, Quaternion.identity);
            ammoInMag--;
            canShoot = false;
            countdown = rateOfFire;
        }
    }
    IEnumerator Reload()
    {
        isReloading = true;
        reloadText.text = "RELOADING";
        canMove = false;
        transform.position = new Vector3(transform.position.x, -17);
        yield return new WaitForSeconds(reloadTime);

        totalAmmo += ammoInMag;
        if (totalAmmo < magSize)
        {
            ammoInMag = totalAmmo;
            totalAmmo -= totalAmmo;
        }
        else
        {
            ammoInMag = magSize;
            totalAmmo -= magSize;
        }
        reloadText.text = "";
        isReloading = false;
        canMove = true;
    }

    public void EquipUpgrade(UpgradeTemplate newUpgrade)
    {
        if (newUpgrade.compatibleGun != gunType)
            return;

        equippedUpgrades.Add(newUpgrade);
        CalculateStats();
    }

    void CalculateStats()
    {
        magSize = gunType.magSize;
        rateOfFire = gunType.rateOfFire;
        reloadTime = gunType.reloadTime;
        
        foreach (UpgradeTemplate upgrade in equippedUpgrades)
        {
            magSize += upgrade.magSize;
            rateOfFire += upgrade.rateOfFire;
            reloadTime += upgrade.reloadTime;
        }
    }

    public void PopulateInfo(GunTemplate newGun)
    {
        gunType = newGun;
        magSize = gunType.magSize;
        rateOfFire = gunType.rateOfFire;
        reloadTime = gunType.reloadTime;
        ammoInMag = gunType.magSize;
        countdown = gunType.rateOfFire;
        crosshairRend.sprite = gunType.crosshair;
        handRend.sprite = gunType.handSprite;
        _animController.anim.runtimeAnimatorController = gunType.animController;
    }
}
