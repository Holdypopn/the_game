using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeEnemy : Enemy
{
    public float damage = 1;
    public float enemyCoolDown = 2;

    private GameObject target;
    private Animator anim;
    private bool playerInRange = false;
    private bool canAttack = true;
    
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;

        target = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        if(!playerInRange)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, this.moveSpeed * Time.deltaTime);*/

        agent.SetDestination(target.transform.position);

        if(playerInRange && canAttack)
        {
            anim.SetTrigger("attack");
            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
            player.TakeDamage(damage);
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
