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

    public float maxDistance = 15f;
    public GameObject projectile;
    public float startTimeBetweenShots;
    public Transform viewPoint;
    public float viewDistance = 10f;

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
        //Debug.Log(CanSeePlayer(viewDistance));

        // wenn inRange aber nicht canSee dann moveCloser

        if(GetRangeToPlayer(player) > maxDistance || !CanSeePlayer())
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
            //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, this.moveSpeed * Time.deltaTime);
        }

        if(GetRangeToPlayer(player) < maxDistance && CanSeePlayer())
            agent.isStopped = true;


        if(timeBetweenShoots <= 0 && CanSeePlayer())
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBetweenShoots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShoots -= Time.deltaTime;
        }
    }

    float GetRangeToPlayer(GameObject player)
    {
        return Vector2.Distance(transform.position, player.transform.position);
    }

    bool CanSeePlayer()
    {
        bool canSeePlayer = false;

        RaycastHit2D hit = Physics2D.Linecast(viewPoint.position, player.transform.position, 1 << LayerMask.NameToLayer("Action"));

        if(hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
                canSeePlayer = true;
            else
                canSeePlayer = false;

            Debug.DrawLine(viewPoint.position, hit.point, Color.red);
        }
        else
            Debug.DrawLine(viewPoint.position, hit.point, Color.yellow);

        return canSeePlayer;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            // using this to destroy the Skull because of ramming into the player
            // probably not a good idea -> floatingText + wrong usage
            float damage = GetComponent<EnemyHealth>().health;
            GetComponent<EnemyHealth>().TakeDamage(damage);

            Player player = col.gameObject.GetComponent<Player>();
            player.TakeDamage(damage);
        }
    }
}
