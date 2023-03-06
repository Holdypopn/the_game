using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : MonoBehaviour
{
    public GameObject fireballPrefab;
    public GameObject blueFireballPrefab;
    public Transform firePoint;
    public float fireForce = 10f;
    private Quaternion rotation;

    public void ShootFireball()
    {
        GameObject projectile = Instantiate(fireballPrefab, firePoint.position, rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }

    public void ShootBlueFireball()
    {
        GameObject projectile = Instantiate(blueFireballPrefab, firePoint.position, rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rotation = firePoint.rotation * Quaternion.Euler(0, 0, 90);
    }
}
