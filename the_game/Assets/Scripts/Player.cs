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
        currentHealth = GameManager.GM.currentHealth;
        maxHealth = GameManager.GM.maxHealth;
        currentShield = GameManager.GM.currentShield;
        maxShield = GameManager.GM.maxShield;
    }

    void Update()
    {
        
    }

    private void UpdateGameManagerData()
    {
        GameManager.GM.currentHealth = currentHealth;
        GameManager.GM.maxHealth = maxHealth;
        GameManager.GM.currentShield = currentShield;
        GameManager.GM.maxShield = maxShield;
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
        UpdateGameManagerData();
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
