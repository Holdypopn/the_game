using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private List<GameObject> weaponPrefabs = new List<GameObject>();
    private List<GameObject> weaponConfigs = new List<GameObject>();

    public Transform firePoint;
    
    private Quaternion rotation;
    private WeaponConfig currentWeaponConfig;
    private bool pickupNewWeapon = false;
    private GameObject toBeDeleted;


    // Start is called before the first frame update
    void Start()
    {
        weaponPrefabs = LoadAllProjectilePrefabs();
        weaponConfigs = LoadAllWeaponConfigs();

        //Set default weapon
        currentWeaponConfig = weaponConfigs.Find(item => item.name == "Wand Of Fire").GetComponent<WeaponConfig>();
        currentWeaponConfig.projectilePrefabPrimary = weaponPrefabs.Find(item => item.name == currentWeaponConfig.projectilePrefabNamePrimary);
        currentWeaponConfig.projectilePrefabSecondary = weaponPrefabs.Find(item => item.name == currentWeaponConfig.projectilePrefabNameSecondary);
    }

    // Update is called once per frame
    void Update()
    {
        currentWeaponConfig.attackTimerPrimary += Time.deltaTime;
        currentWeaponConfig.attackTimerSecondary += Time.deltaTime;
        rotation = firePoint.rotation * Quaternion.Euler(0, 0, 90);

        if(pickupNewWeapon && Input.GetKeyDown(KeyCode.F))
        {

            var currentWeapon = weaponConfigs.Find(item => item.name == currentWeaponConfig.weaponName);
            Instantiate(currentWeapon, transform.position, Quaternion.identity);

            currentWeaponConfig = toBeDeleted.GetComponent<WeaponConfig>();
            currentWeaponConfig.projectilePrefabPrimary = weaponPrefabs.Find(item => item.name == currentWeaponConfig.projectilePrefabNamePrimary);
            currentWeaponConfig.projectilePrefabSecondary = weaponPrefabs.Find(item => item.name == currentWeaponConfig.projectilePrefabNameSecondary);
            pickupNewWeapon = false;
            Destroy(toBeDeleted);
        }        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Weapon"))
        {
            pickupNewWeapon = true;
            toBeDeleted = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Weapon"))
        {
            pickupNewWeapon = false;
            toBeDeleted = null;
        }
    }

    public List<GameObject> LoadAllProjectilePrefabs()
    {
        Object[] tempWeaponPrefabArray = Resources.LoadAll("Projectiles", typeof(GameObject));

        foreach(GameObject weaponPrefab in tempWeaponPrefabArray)
        {
            weaponPrefabs.Add(weaponPrefab);
        }

        return weaponPrefabs;
    }

    public List<GameObject> LoadAllWeaponConfigs()
    {
        Object[] tempWeaponConfigArray = Resources.LoadAll("WeaponConfigs", typeof(GameObject));

        foreach(GameObject weaponConfig in tempWeaponConfigArray)
        {
            weaponConfigs.Add(weaponConfig);
        }

        return weaponConfigs;
    }

    public void attackPrimary()
    {
        if(currentWeaponConfig.attackTimerPrimary > currentWeaponConfig.coolDownTimerPrimary && currentWeaponConfig.weaponName != null)
        {
            currentWeaponConfig.attackTimerPrimary = 0;
            GameObject projectile = Instantiate(currentWeaponConfig.projectilePrefabPrimary, firePoint.position, rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * currentWeaponConfig.projectileSpeedPrimary, ForceMode2D.Impulse);
        }
    }

    public void attackSecondary()
    {
        if(currentWeaponConfig.attackTimerSecondary > currentWeaponConfig.coolDownTimerSecondary && currentWeaponConfig.weaponName != null)
        {
            currentWeaponConfig.attackTimerSecondary = 0;
            GameObject projectile = Instantiate(currentWeaponConfig.projectilePrefabSecondary, firePoint.position, rotation);
            projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * currentWeaponConfig.projectileSpeedSecondary, ForceMode2D.Impulse);
        }
    }
}