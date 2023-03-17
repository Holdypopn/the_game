using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public float currentShield;
    public float maxShield;
    public GameObject floatingTextPrefab;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        ShowDamage(damageAmount);
        if(currentShield <= 0)
            currentHealth -= damageAmount;
        else
        {
            currentShield -= damageAmount;
            if(currentShield <= 0)
            {
                currentHealth -= currentShield;
                currentShield = 0;
            }
        }
    }

    void ShowDamage(float damage)
    {
        if(floatingTextPrefab)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = damage.ToString();
        }
    }
}
