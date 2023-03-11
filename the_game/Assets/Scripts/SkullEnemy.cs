using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullEnemy : Enemy
{
    private GameObject target;
    private Animator anim;
    private bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
