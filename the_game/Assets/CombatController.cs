using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public static List<GameObject> weaponPrefabs = new List<GameObject>();

    public Transform firePoint;
    public GameObject fireballPrefab;
    public GameObject blueFireballPrefab;

    
    private Quaternion rotation;
    private WeaponConfig currentWeaponConfig;



    // Start is called before the first frame update
    void Start()
    {
        LoadAllWeaponPrefabs();

        //Testing
        currentWeaponConfig.weaponName = "Wand of Fire";
        currentWeaponConfig.attackTimerPrimary = 0.1f;
        currentWeaponConfig.coolDownTimerPrimary = 0.5f;
        currentWeaponConfig.projectileSpeedPrimary = 30f;
        currentWeaponConfig.projectilePrefabNamePrimary = "redFireball";
        currentWeaponConfig.projectilePrefabPrimary = weaponPrefabs.Find(item => item.name == currentWeaponConfig.projectilePrefabNamePrimary);

        currentWeaponConfig.weaponName = "Wand of Fire";
        currentWeaponConfig.attackTimerSecondary = 0.1f;
        currentWeaponConfig.coolDownTimerSecondary = 0.2f;
        currentWeaponConfig.projectileSpeedSecondary = 30f;
        currentWeaponConfig.projectilePrefabNameSecondary = "blueFireball";
        currentWeaponConfig.projectilePrefabSecondary = weaponPrefabs.Find(item => item.name == currentWeaponConfig.projectilePrefabNameSecondary);
    }

    // Update is called once per frame
    void Update()
    {
        currentWeaponConfig.attackTimerPrimary += Time.deltaTime;
        currentWeaponConfig.attackTimerSecondary += Time.deltaTime;
        rotation = firePoint.rotation * Quaternion.Euler(0, 0, 90);
    }

    void LoadAllWeaponPrefabs()
    {
        Object[] tempWeaponPrefabArray = Resources.LoadAll("Prefabs", typeof(GameObject));

        foreach(GameObject weaponPrefab in tempWeaponPrefabArray)
        {
            weaponPrefabs.Add(weaponPrefab);
        }
    }

    public void attackPrimary()
    {
        if(currentWeaponConfig.attackTimerPrimary > currentWeaponConfig.coolDownTimerPrimary)
        {
            currentWeaponConfig.attackTimerPrimary = 0;
            GameObject projectile = Instantiate(currentWeaponConfig.projectilePrefabPrimary, firePoint.position, rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * currentWeaponConfig.projectileSpeedPrimary, ForceMode2D.Impulse);
        }
    }

    public void attackSecondary()
    {
        if(currentWeaponConfig.attackTimerSecondary > currentWeaponConfig.coolDownTimerSecondary)
        {
            currentWeaponConfig.attackTimerSecondary = 0;
            GameObject projectile = Instantiate(currentWeaponConfig.projectilePrefabSecondary, firePoint.position, rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * currentWeaponConfig.projectileSpeedSecondary, ForceMode2D.Impulse);
        }
    }
}



public struct WeaponConfig
{
    public string weaponName;

    public float attackTimerPrimary;
    public float coolDownTimerPrimary;
    public float projectileSpeedPrimary;
    public string projectilePrefabNamePrimary;
    public GameObject projectilePrefabPrimary;

    public float attackTimerSecondary;
    public float coolDownTimerSecondary;
    public float projectileSpeedSecondary;
    public string projectilePrefabNameSecondary;
    public GameObject projectilePrefabSecondary;
}

public class Weapon : MonoBehaviour
{
    private WeaponConfig weaponConfig;

    public WeaponConfig getWeaponConfig()
    {
        return weaponConfig;
    }

    public Weapon()
    {
        
    }

    public void attackPrimary()
    {
        if(weaponConfig.attackTimerPrimary > weaponConfig.coolDownTimerPrimary)
        {
            weaponConfig.attackTimerPrimary = 0;
            //GameObject projectile = Instantiate(weaponConfig.projectilePrefabPrimary, firePoint.position, rotation);
            //projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }
    }
}
/*
public class FireballWand : Weapon
{
    public FireballWand()
    {
        WeaponConfig config = getWeaponConfig();
        config.weaponName = "Fireball Wand";
        config.attackTimer = 0.1f;
        config.coolDownTimer = 0.5f;
    }
}

public class BlueFireballWand : Weapon
{
    public BlueFireballWand()
    {
        WeaponConfig config = getWeaponConfig();
        config.weaponName = "Blue Fireball Wand";
        config.attackTimer = 0.1f;
        config.coolDownTimer = 0.2f;
    }
}*/