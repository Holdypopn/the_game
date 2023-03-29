using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkullEnemy : Enemy
{
    private GameObject player;
    private Animator anim;
    private float timeBetweenShoots;
    private NavMeshAgent agent;

    public GameObject projectile;
    public float startTimeBetweenShots;
    public float viewDistance = 15f;
    public Transform viewPoint;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;

        player = GameObject.FindWithTag("Player");
        //anim = GetComponent<Animator>();

        timeBetweenShoots = startTimeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetRangeToPlayer(player) > viewDistance || !CanSeePlayer(viewPoint, player))
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
            //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, this.moveSpeed * Time.deltaTime);
        }

        if(GetRangeToPlayer(player) < viewDistance && CanSeePlayer(viewPoint, player))
            agent.isStopped = true;


        if(timeBetweenShoots <= 0 && CanSeePlayer(viewPoint, player))
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBetweenShoots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShoots -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            // using this to destroy the Skull because of ramming into the player
            // probably not a good idea -> floatingText + wrong usage
            // FIX: There is no ramming anymore
            float damage = GetComponent<EnemyHealth>().health;
            GetComponent<EnemyHealth>().TakeDamage(damage);

            Player player = col.gameObject.GetComponent<Player>();
            player.TakeDamage(damage);
        }
    }
}
