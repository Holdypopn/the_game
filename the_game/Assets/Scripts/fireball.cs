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

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage(damage);
            anim.SetTrigger("onDeath");
            
            Destroy(gameObject, 0.3f);
        }
    }
}
