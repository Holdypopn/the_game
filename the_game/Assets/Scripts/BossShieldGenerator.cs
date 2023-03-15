using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShieldGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<EnemyHealth>().health <= 0)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    
}
