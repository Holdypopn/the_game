using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBoss : MonoBehaviour
{
    private float timeBetweenShots;

    public float startTimeBetweenShots;
    public GameObject projectile;

    public GameObject BossShieldGenerator1;
    public GameObject BossShieldGenerator2;
    public GameObject BossShieldGenerator3;
    public GameObject BossShieldGenerator4;
    public GameObject shield;

    private List<GameObject> generatorList;
    private bool phaseDone = false;

    // Start is called before the first frame update
    void Start()
    {
        timeBetweenShots = startTimeBetweenShots;
        generatorList = new List<GameObject>();
        generatorList.Add(BossShieldGenerator1);
        generatorList.Add(BossShieldGenerator2);
        generatorList.Add(BossShieldGenerator3);
        generatorList.Add(BossShieldGenerator4);
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
}