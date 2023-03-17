using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBoss : Enemy
{
    public float startTimeBetweenShots;
    public GameObject projectile;
    public GameObject BossShieldGenerator1;
    public GameObject BossShieldGenerator2;
    public GameObject BossShieldGenerator3;
    public GameObject BossShieldGenerator4;
    public GameObject shield;
    public float meleeAttackCoolDown = 2;

    private float timeBetweenShots;
    private List<GameObject> generatorList;
    private bool phaseDone = false;
    private float touchDamage = 10f;
    private bool playerInRange = false;
    private bool canAttack = true;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        timeBetweenShots = startTimeBetweenShots;
        generatorList = new List<GameObject>();
        generatorList.Add(BossShieldGenerator1);
        generatorList.Add(BossShieldGenerator2);
        generatorList.Add(BossShieldGenerator3);
        generatorList.Add(BossShieldGenerator4);

        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBetweenShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
            timeBetweenShots -= Time.deltaTime;
            
        if(GetComponent<EnemyHealth>().health <= (GetComponent<EnemyHealth>().maxHealth / 2) && !phaseDone)
        {
            phaseDone = true;
            foreach (var generator in generatorList)
                generator.GetComponent<SpriteRenderer>().color = Color.blue;
            shield.SetActive(true);
            GetComponent<Collider2D>().enabled = false;
        }

        if(CheckIfGeneratorsAlive())
        {
            shield.SetActive(false);
            GetComponent<Collider2D>().enabled = true;
        }

        if(playerInRange && canAttack)
        {
            player.TakeDamage(touchDamage);
            StartCoroutine(AttackCoolDown());
        }
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

    public bool CheckIfGeneratorsAlive()
    {
        bool allGeneratorsDead = false;
        foreach (var generator in generatorList)
        {
            if(generator.gameObject.GetComponent<EnemyHealth>().health <= 0)
                allGeneratorsDead = true;
            else
            {
                allGeneratorsDead = false;
                return allGeneratorsDead;
            }
        }
        return allGeneratorsDead;
    }

    IEnumerator AttackCoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(meleeAttackCoolDown);
        canAttack = true;
    }
}