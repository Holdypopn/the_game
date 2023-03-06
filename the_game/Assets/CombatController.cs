using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public Weapon weapon;
    public Transform firePoint;
    private Quaternion rotation;
    public GameObject fireballPrefab;
    public GameObject blueFireballPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotation = firePoint.rotation * Quaternion.Euler(0, 0, 90);

        if(Input.GetMouseButtonDown(0))
            weapon.attack();
    }
}



public struct WeaponConfig
{
    public string weaponName;
    public float attackTimer;
    public float coolDownTimer;
    public GameObject projectilePrefab;
}

public class Weapon : MonoBehaviour
{
    private WeaponConfig weaponConfig;

    public WeaponConfig getWeaponConfig()
    {
        return weaponConfig;
    }

    public void attack()
    {
        if(weaponConfig.attackTimer > weaponConfig.coolDownTimer)
        {
            weaponConfig.attackTimer = 0;
            //GameObject projectile = Instantiate(weaponConfig.projectilePrefab, firePoint.position, rotation);
            //projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }
    }
}

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
}