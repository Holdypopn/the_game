using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeEnemy : Enemy
{
    public float damage = 1;
    public float enemyCoolDown = 2;
    public Transform viewPoint;
    public float viewDistance = 15f;

    private GameObject player;
    private Animator anim;
    private bool playerInRange = false;
    private bool canAttack = true;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;

        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        if(!playerInRange)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, this.moveSpeed * Time.deltaTime);*/
        if(GetRangeToPlayer(player) <= viewDistance && CanSeePlayer(viewPoint, player))
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }

        if(GetRangeToPlayer(player) >= viewDistance)
            agent.isStopped = true;

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
