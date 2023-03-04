using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> OnEnemyKilled;
    public float health, maxHealth = 3f;
    public float moveSpeed = 10;
    public float damage = 1;
    public float enemyCoolDown = 2;

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
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);

        if(playerInRange && canAttack)
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().currentHealth -= damage;
            StartCoroutine(AttackCoolDown());
        }
    }

    IEnumerator AttackCoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(enemyCoolDown);
        canAttack = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
            playerInRange = false;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if(health <= 0)
        {
            Destroy(gameObject, 0.3f);
            anim.SetTrigger("onDeath");
            moveSpeed = 0;
            OnEnemyKilled?.Invoke(this);
        }
    }
}
