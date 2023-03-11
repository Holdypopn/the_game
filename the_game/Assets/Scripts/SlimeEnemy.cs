using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : Enemy
{
    public float damage = 1;
    public float enemyCoolDown = 2;

    private GameObject target;
    private Animator anim;
    private bool playerInRange = false;
    private bool canAttack = true;
    

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        if(!playerInRange)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, this.moveSpeed * Time.deltaTime);

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
}
