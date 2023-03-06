using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const float dropCHance = 1f / 10f;
    public static event Action<Enemy> OnEnemyKilled;
    public float health, maxHealth = 3f;
    public float moveSpeed = 10;
    public float damage = 1;
    public float enemyCoolDown = 2;
    public GameObject drop;
    public GameObject floatingTextPrefab;

    private GameObject target;
    private Animator anim;
    private bool playerInRange = false;
    private bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        target = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        if(!playerInRange)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);

        if(playerInRange && canAttack)
        {
            anim.SetTrigger("attack");

            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
            if(player.currentShield <= 0)
                player.currentHealth -= damage;
            else
            {
                player.currentShield -= damage;
                if(player.currentShield <= 0)
                {
                    player.currentHealth -= player.currentShield;
                    player.currentShield = 0;
                }
            }

            StartCoroutine(AttackCoolDown());
        }
    }

    IEnumerator AttackCoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(enemyCoolDown);
        canAttack = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
            playerInRange = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
            playerInRange = false;
    }

    public void TakeDamage(float damageAmount)
    {
        ShowDamage(damageAmount.ToString());
        health -= damageAmount;

        if(health <= 0)
        {
            Destroy(gameObject, 0.3f);
            anim.SetTrigger("onDeath");
            moveSpeed = 0;
            OnEnemyKilled?.Invoke(this);
            if(UnityEngine.Random.Range(0f, 1f) <= dropCHance)
                Instantiate(drop, transform.position, Quaternion.identity);
        }
    }

    void ShowDamage(string text)
    {
        if(floatingTextPrefab)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }
}
