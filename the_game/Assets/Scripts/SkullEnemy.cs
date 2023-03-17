using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullEnemy : Enemy
{
    private GameObject player;
    private Animator anim;
    private float timeBetweenShoots;


    public float maxDistance = 15f;
    public GameObject projectile;
    public float startTimeBetweenShots;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //anim = GetComponent<Animator>();

        timeBetweenShoots = startTimeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetRangeToPlayer(player) > maxDistance)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, this.moveSpeed * Time.deltaTime);

        if(GetRangeToPlayer(player) <= maxDistance / 3)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, this.moveSpeed * Time.deltaTime);


        if(timeBetweenShoots <= 0)
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
