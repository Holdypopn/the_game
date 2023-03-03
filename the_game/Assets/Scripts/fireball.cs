using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    public float damage = 1;
    private Animator anim;

    // Start is called before the first frame update
    void OnBecameInvisible() 
    {
         Destroy(gameObject);
    }
   
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if(col.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(damage);
            anim.SetTrigger("onDeath");
            Destroy(gameObject, 0.5f);
        }
    }
}
