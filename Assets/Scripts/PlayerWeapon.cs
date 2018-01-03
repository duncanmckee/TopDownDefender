using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour {

    public enum WeaponType
    {
        None, Fireball, Crossbow, EggCannon, Peashooter, PlasmaRifle
    }

    public WeaponType leftWeapon = WeaponType.Fireball;
    private float leftTimer = 0f;
    private float leftCooldown = 0f;
    public WeaponType rightWeapon = WeaponType.None;
    private float rightTimer = 0f;
    private float rightCooldown = 0f;

    private bool upgradeInteraction = false;
    private GameObject currentUpgrade;

    public GameObject bulletSpawn;
    public GameObject FireBoltPrefab;
    public GameObject CrossBowBoltPrefab;
    public GameObject EggPrefab;
    public GameObject PeaPrefab;
    public GameObject PlasmaRifle;

    private Text Weapon1;
    private Text Weapon2;

    private GameObject bolt;

    // Use this for initialization
    void Start () {
        Weapon1 = GameObject.Find("Weapon 1").GetComponent<Text>();
        Weapon2 = GameObject.Find("Weapon 2").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        Weapon1.text = leftWeapon.ToString();
        Weapon2.text = rightWeapon.ToString();
        UpdateCooldownTime(leftWeapon, out leftCooldown);
        UpdateCooldownTime(rightWeapon, out rightCooldown);
        if(Input.GetAxis("Horizontal2")* Input.GetAxis("Horizontal2") + Input.GetAxis("Vertical2") * Input.GetAxis("Vertical2") > 0.8)
        {
            bulletSpawn.transform.position = new Vector2(this.transform.position.x + Input.GetAxis("Horizontal2"), this.transform.position.y + Input.GetAxis("Vertical2"));
        }
        bulletSpawn.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * (Mathf.Atan2(Input.GetAxis("Vertical2"), Input.GetAxis("Horizontal2"))));
        if (leftWeapon != WeaponType.None)
        {
            if (Input.GetAxis("LTrigger") >= 0.8 && leftTimer + leftCooldown < Time.time)
            {
                ShootWeapon(leftWeapon);
                leftTimer = Time.time;
            }
        }

        if (rightWeapon != WeaponType.None)
        {
            if (Input.GetAxis("RTrigger") >= 0.8 && rightTimer + rightCooldown < Time.time)
            {
                ShootWeapon(rightWeapon);
                rightTimer = Time.time;
            }
        }

        if (upgradeInteraction && Input.GetAxis("LBumper") == 1)
        {
            Debug.Log("Upgrade Left: " + currentUpgrade.GetComponent<UpgradeRandomizer>().upgradeName);
            switch (currentUpgrade.GetComponent<UpgradeRandomizer>().upgradeName)
            {
                case "Fireball":
                    leftWeapon = WeaponType.Fireball;
                    break;
                case "EggCannon":
                    leftWeapon = WeaponType.EggCannon;
                    break;
                case "Crossbow":
                    leftWeapon = WeaponType.Crossbow;
                    break;
                case "Peashooter":
                    leftWeapon = WeaponType.Peashooter;
                    break;
                case "PlasmaRifle":
                    leftWeapon = WeaponType.PlasmaRifle;
                    break;
            }
            Destroy(currentUpgrade);
        }
        else if(upgradeInteraction && Input.GetAxis("RBumper") == 1)
        {
            Debug.Log("Upgrade Right: " + currentUpgrade.GetComponent<UpgradeRandomizer>().upgradeName);
            switch (currentUpgrade.GetComponent<UpgradeRandomizer>().upgradeName)
            {
                case "Fireball":
                    rightWeapon = WeaponType.Fireball;
                    break;
                case "EggCannon":
                    rightWeapon = WeaponType.EggCannon;
                    break;
                case "Crossbow":
                    rightWeapon = WeaponType.Crossbow;
                    break;
                case "Peashooter":
                    rightWeapon = WeaponType.Peashooter;
                    break;
                case "PlasmaRifle":
                    rightWeapon = WeaponType.PlasmaRifle;
                    break;
            }
            Destroy(currentUpgrade);
        }
	}

    void ShootWeapon(WeaponType weapon)
    {
        switch (weapon)
        {
            case WeaponType.None:
                break;
            case WeaponType.Fireball:
                bolt = Instantiate(FireBoltPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                bolt.GetComponent<Rigidbody2D>().velocity = (bulletSpawn.transform.position - this.transform.position) * 8;
                break;
            case WeaponType.Crossbow:
                bolt = Instantiate(CrossBowBoltPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                bolt.GetComponent<Rigidbody2D>().velocity = (bulletSpawn.transform.position - this.transform.position) * 10;
                break;
            case WeaponType.EggCannon:
                bolt = Instantiate(EggPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                bolt.GetComponent<Rigidbody2D>().velocity = (bulletSpawn.transform.position - this.transform.position) * 5;
                break;
            case WeaponType.Peashooter:
                bolt = Instantiate(PeaPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                bolt.GetComponent<Rigidbody2D>().velocity = (bulletSpawn.transform.position - this.transform.position) * 6;
                break;
            case WeaponType.PlasmaRifle:
                bolt = Instantiate(PlasmaRifle, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                bolt.GetComponent<Rigidbody2D>().velocity = (bulletSpawn.transform.position - this.transform.position) * 12;
                break;
        }
    }

    void UpdateCooldownTime(WeaponType weapon, out float cooldown)
    {
        switch (weapon)
        {
            case WeaponType.None:
                cooldown = 0f;
                break;
            case WeaponType.Fireball:
                cooldown = 0.5f;
                break;
            case WeaponType.Crossbow:
                cooldown = 1f;
                break;
            case WeaponType.EggCannon:
                cooldown = 1.5f;
                break;
            case WeaponType.Peashooter:
                cooldown = 0.1f;
                break;
            case WeaponType.PlasmaRifle:
                cooldown = 3.0f;
                break;
            default:
                cooldown = 0f;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Upgrade")
        {
            upgradeInteraction = true;
            currentUpgrade = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Upgrade")
        {
            upgradeInteraction = false;
            currentUpgrade = null;
        }
    }
}
