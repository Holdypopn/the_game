using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public float currentShield;
    public float maxShield;

    void Start()
    {
        currentHealth = maxHealth;
    }
}
