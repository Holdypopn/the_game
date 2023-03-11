using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    private List<GameObject> weaponPrefabs = new List<GameObject>();

    public Transform firePoint;
    
    private Quaternion rotation;
    private WeaponConfig currentWeaponConfig;
    public bool pickupNewWeapon = false;


    // Start is called before the first frame update
    void Start()
    {
        weaponPrefabs = LoadAllWeaponPrefabs();

        //Testing
        currentWeaponConfig.weaponName = null;
        currentWeaponConfig.attackTimerPrimary = 0.1f;
        currentWeaponConfig.coolDownTimerPrimary = 0.5f;
        currentWeaponConfig.projectileSpeedPrimary = 30f;
        currentWeaponConfig.projectilePrefabNamePrimary = "redFireball";
        currentWeaponConfig.projectilePrefabPrimary = weaponPrefabs.Find(item => item.name == currentWeaponConfig.projectilePrefabNamePrimary);

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

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Weapon"))
        {

        }
    }

    public List<GameObject> LoadAllWeaponPrefabs()
    {
        Object[] tempWeaponPrefabArray = Resources.LoadAll("Prefabs", typeof(GameObject));

        foreach(GameObject weaponPrefab in tempWeaponPrefabArray)
        {
            weaponPrefabs.Add(weaponPrefab);
        }

        return weaponPrefabs;
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