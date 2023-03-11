using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullEnemy : Enemy
{
    private GameObject player;
    private Animator anim;
    private bool playerInRange = false;

    public float maxDistance = 15f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetRangeToPlayer(player) > maxDistance)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, this.moveSpeed * Time.deltaTime);

        if(GetRangeToPlayer(player) <= maxDistance / 3)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, this.moveSpeed * Time.deltaTime);

    }

    float GetRangeToPlayer(GameObject player)
    {
        return Vector2.Distance(transform.position, player.transform.position);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            float damage = GetComponent<EnemyHealth>().health;
            GetComponent<EnemyHealth>().TakeDamage(damage);
            Player player = col.gameObject.GetComponent<Player>();
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

        }
    }
}
