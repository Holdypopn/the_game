using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float dropChance = 1f / 10f;
    public float health, maxHealth = 10f;
    public Enemy enemy;
    public GameObject drop;
    public GameObject floatingTextPrefab;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageAmount)
    {
        ShowDamage(damageAmount);
        health -= damageAmount;

        if(health <= 0)
        {
            if(gameObject.tag != "Structure")
            {
                Destroy(gameObject, 0.3f);
                anim.SetTrigger("onDeath");
                enemy.moveSpeed = 0;
                if(UnityEngine.Random.Range(0f, 1f) <= dropChance)
                    Instantiate(drop, transform.position, Quaternion.identity);
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
